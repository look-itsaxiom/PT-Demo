using Godot;
using Godot.Collections;
using CharacterData;
using System;
using ChronosSpace;
using TownResources;

public partial class TownNPC : CharacterBody3D
{
    public Character MyCharacter;
    public NavigationAgent3D NavigationAgent;
    public float MoveSpeed = 3.5f;
    public AnimationPlayer AnimationPlayer;
    public bool IsMoving = false;
    public bool IsInteracting = false;
    public bool NewToTown = true;
    public bool InTown = true;
    public Quest MyQuest = null;
    private float MovementDelta;
    private EnvGridMap EnvGridMap;


    public void Initialize(Character character)
    {
        MyCharacter = character;
        var characterModel = character.CharacterModel.Instantiate();
        AnimationPlayer = characterModel.GetNode<AnimationPlayer>("AnimationPlayer");
        AddChild(characterModel);
    }

    public override void _Ready()
    {
        NavigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        NavigationAgent.TargetReached += OnTargetReached;

        GameSignalBus.Instance.Connect(GameSignalBus.SignalName.NPCStartedQuest, Callable.From<Quest, Character>(OnQuestStarted));
        GameSignalBus.Instance.Connect(GameSignalBus.SignalName.QuestCompleted, Callable.From<Quest, Character>(OnQuestCompleted));
        EnvGridMap = GetParent().GetNode<EnvGridMap>("EnvGridMap");
        OnTownArrival();

    }

    private void OnTargetReached()
    {
        if (!InTown)
            return;

        AnimationPlayer.Play("Interact");
        IsMoving = false;
        Timer timer = new Timer();
        timer.WaitTime = GD.RandRange(3.0f, 5.0f);
        timer.OneShot = true;
        timer.Timeout += () =>
        {
            var newPoint = EnvGridMap.GetRandomTownPoint();
            NavigationAgent.TargetPosition = newPoint;
            timer.QueueFree();
        };
        AddChild(timer);
        timer.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsInteracting || !InTown)
        {
            IsMoving = false;
            return;
        }

        Vector3 nextPathPosition = NavigationAgent.GetNextPathPosition();

        if (NavigationAgent.IsNavigationFinished() == true)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
            nextPathPosition.Y = GlobalPosition.Y;

            Vector3 directionToTarget = (nextPathPosition - GlobalPosition).Normalized();
            Velocity = directionToTarget * MoveSpeed;

            NavigationAgent.Velocity = Velocity;

            RotateTo(directionToTarget);

            MoveAndSlide();
        }
    }

    public override void _Process(double delta)
    {
        if (IsInteracting || !InTown)
        {
            return;
        }

        if (IsMoving)
        {
            AnimationPlayer.Play("Walking_A");
        }
        else
        {
            AnimationPlayer.Play("Idle");
        }
    }

    public void RotateTo(Vector3 directionToTarget)
    {
        if (directionToTarget.LengthSquared() > 0.01f)
        {
            var targetRotation = new Vector3(0, Mathf.Atan2(directionToTarget.X, directionToTarget.Z), 0);
            Rotation = Rotation.Lerp(targetRotation, 5f * (float)GetPhysicsProcessDeltaTime());
        }
    }

    private void OnTownArrival()
    {
        if (NewToTown)
        {
            Timer timer = new Timer();
            timer.WaitTime = 3.0f;
            timer.OneShot = true;
            timer.Timeout += () =>
            {
                var cheerAnimation = AnimationPlayer.GetAnimation("Cheer");
                cheerAnimation.LoopMode = Animation.LoopModeEnum.None;
                OnTownArrivalFinished();
                timer.QueueFree();
            };
            AddChild(timer);
            timer.Start();

            var cheerAnimation = AnimationPlayer.GetAnimation("Cheer");
            cheerAnimation.LoopMode = Animation.LoopModeEnum.Linear;
            AnimationPlayer.Play("Cheer");
        }
        else
        {
            OnTownArrivalFinished();
        }

    }

    private void OnTownArrivalFinished()
    {
        AnimationPlayer.Play("Idle");
        NewToTown = false;
        OnTargetReached();
    }

    private void OnQuestStarted(Quest quest, Character character)
    {
        if (character == MyCharacter)
        {
            MyQuest = quest;
            var townExitPos = GetParent().GetNode<Node3D>("TownExit").Position;
            NavigationAgent.TargetPosition = new Vector3(townExitPos.X, EnvGridMap.BaseY, townExitPos.Z);
            NavigationAgent.TargetReached -= OnTargetReached;
            NavigationAgent.TargetReached += ExitTown;
        }
    }

    private void OnQuestCompleted(Quest quest, Character character)
    {
        if (character == MyCharacter)
        {
            MyQuest = null;
            InTown = true;
            var townExitPos = GetParent().GetNode<Node3D>("TownExit").Position;
            NavigationAgent.TargetPosition = new Vector3(townExitPos.X, EnvGridMap.BaseY, townExitPos.Z + 5);
            Visible = true;
            NavigationAgent.TargetReached -= ExitTown;
            NavigationAgent.TargetReached += OnTargetReached;
            GD.Print($"NPC {MyCharacter.CharacterName} has returned to the town.");
        }
    }

    private void ExitTown()
    {
        GD.Print($"NPC {MyCharacter.CharacterName} has left the town to start their quest.");
        Visible = false;
        InTown = false;
        var timeHook = new ChronosTimeHook
        {
            TriggerDay = Chronos.Instance.CurrentDay,
            TriggerTime = Chronos.Instance.CurrentTime + 10f,
            ReturnSignal = GameSignalBus.SignalName.ResourceCollected,
            ReturnSignalArgs = new Array<Variant>() {
                new ResourceCollectEvent
                {
                    EventName = "QuestCompleted",
                    AttributedCharacter = MyCharacter,
                    EventData = new TownResource
                    {
                        ResourceKey = ResourceType.Terratite,
                        Amount = 5
                    }
                }
            }
        };
        GD.Print($"Registering time hook {timeHook.ReturnSignal} for NPC {MyCharacter.CharacterName}.");
        GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.RegisterTimeHook, timeHook);
    }
}

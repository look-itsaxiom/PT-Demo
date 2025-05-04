using Godot;
using CharacterData;
using System;

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

        OnTownArrival();

    }

    private void OnTargetReached()
    {
        GD.Print($"{MyQuest?.QuestName} NPC {MyCharacter.CharacterName} has reached the exit point");

        if (MyQuest != null)
        {
            GD.Print($"{MyQuest?.QuestName} NPC {MyCharacter.CharacterName} has reached the exit point");
            ExitTown();
        }

        if (!InTown)
            return;

        AnimationPlayer.Play("Interact");
        Timer timer = new Timer();
        timer.WaitTime = GD.RandRange(3.0f, 5.0f);
        timer.OneShot = true;
        timer.Timeout += () =>
        {
            var newPoint = GetParent().GetNode<EnvGridMap>("EnvGridMap").GetRandomTownPoint();
            NavigationAgent.TargetPosition = newPoint;
            timer.QueueFree();
        };
        AddChild(timer);
        timer.Start();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsInteracting)
        {
            return;
        }

        if (NavigationAgent.IsNavigationFinished())
        {
            AnimationPlayer.Play("Idle");
            Velocity = Vector3.Zero;
            MoveAndSlide();
            return;
        }

        Vector3 nextPathPosition = NavigationAgent.GetNextPathPosition();
        nextPathPosition.Y = GlobalPosition.Y;

        Vector3 directionToTarget = (nextPathPosition - GlobalPosition).Normalized();
        Velocity = directionToTarget * MoveSpeed;

        NavigationAgent.Velocity = Velocity;

        MoveAndSlide();

        RotateTo(delta, directionToTarget);

        AnimationPlayer.Play("Walking_A");
        IsMoving = true;
    }

    public void RotateTo(double delta, Vector3 directionToTarget)
    {
        if (directionToTarget.LengthSquared() > 0.01f)
        {
            var targetRotation = new Vector3(0, Mathf.Atan2(directionToTarget.X, directionToTarget.Z), 0);
            Rotation = Rotation.Lerp(targetRotation, (float)delta * 5.0f);
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
            GD.Print($"NPC {MyCharacter.CharacterName} started quest: {quest.QuestName}");
            MyQuest = quest;
            NavigationAgent.TargetPosition = GetParent().GetNode<Node3D>("TownExit").Position + new Vector3(0, 0, -1);
        }
    }

    private void ExitTown()
    {
        GD.Print($"NPC {MyCharacter.CharacterName} has left the town to start their quest.");
        Visible = false;
        InTown = false;
        //NavigationAgent.TargetReached -= ExitTown;
        Timer timer = new Timer();
        timer.WaitTime = 10.0f;
        timer.OneShot = true;
        timer.Autostart = true;
        timer.Timeout += () =>
        {
            GameSignalBus.Instance.EmitSignal(GameSignalBus.SignalName.ResourceCollected, "Wood", 5);
            InTown = true;
            NavigationAgent.TargetPosition = GetParent().GetNode<Node3D>("TownExit").Position + new Vector3(0, 0, 10);
            Visible = true;
            timer.QueueFree();
        };
        AddChild(timer);
    }
}

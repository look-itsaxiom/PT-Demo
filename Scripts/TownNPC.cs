using Godot;
using CharacterData;
using System;

public partial class TownNPC : CharacterBody3D
{
    public Character Character;
    public CharacterInfoMenu CharacterInfoMenu;
    public NavigationAgent3D NavigationAgent;
    public float MoveSpeed = 3.5f;
    public AnimationPlayer AnimationPlayer;
    public Interactable InteractionArea;
    public CollisionShape3D InteractionCollisionShape;
    public bool IsMoving = false;
    public bool IsInteracting = false;
    public bool NewToTown = true;
    private float MovementDelta;

    public void Initialize(Character character)
    {
        Character = character;
        var characterModel = character.CharacterModel.Instantiate();
        AnimationPlayer = characterModel.GetNode<AnimationPlayer>("AnimationPlayer");
        AddChild(characterModel);
    }

    public override void _Ready()
    {
        NavigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
        NavigationAgent.TargetReached += OnTargetReached;
        InteractionArea = GetNode<Area3D>("InteractionArea") as Interactable;
        InteractionCollisionShape = GetNode<CollisionShape3D>("InteractionArea/CollisionShape3D");
        OnTownArrival();

        // // Set up the interaction area
        // InteractionArea.InteractionStarted += OnInteractionStarted;
        // InteractionArea.InteractionEnded += OnInteractionEnded;
    }

    private void OnTargetReached()
    {
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
        if (NavigationAgent.IsNavigationFinished())
        {
            AnimationPlayer.Play("Idle");
            Velocity = Vector3.Zero;
            MoveAndSlide();
            return;
        }

        Vector3 nextPathPosition = NavigationAgent.GetNextPathPosition();
        nextPathPosition.Y = GlobalPosition.Y; // Keep NPC flat

        Vector3 directionToTarget = (nextPathPosition - GlobalPosition).Normalized();
        Velocity = directionToTarget * MoveSpeed;

        NavigationAgent.Velocity = Velocity; // <-- correct way in Godot 4!

        MoveAndSlide();

        if (directionToTarget.LengthSquared() > 0.01f)
        {
            var targetRotation = new Vector3(0, Mathf.Atan2(directionToTarget.X, directionToTarget.Z), 0);
            Rotation = Rotation.Lerp(targetRotation, (float)delta * 5.0f);
        }

        AnimationPlayer.Play("Walking_A");
        IsMoving = true;
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

    public void Interact(Player player)
    {
        player.CanMove = false;
        var CharacterInfoMenu = GetNode<CharacterInfoMenu>("/root/CharacterInfoMenu");
        CharacterInfoMenu.OnClose += () => player.CanMove = true;
        CharacterInfoMenu.ShowCharacterInfo(Character);
    }
}

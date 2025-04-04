using Unity.VisualScripting;
using UnityEngine;

public class Player1 : Player
{
   public PlayerStateMachine stateMachine { get; private set; }

   public PlayerIdleState idleState { get; private set; }
   public PlayerMoveState moveState { get; private set; }

   private void Awake()
   {
      stateMachine = new PlayerStateMachine();
      idleState = new PlayerIdleState(this, stateMachine, "Idle");
      moveState = new PlayerMoveState(this, stateMachine, "Move");
   }

   protected override void Start()
   {
      stateMachine.Initialize(idleState);
   }

   protected override void Update()
   {
      stateMachine.currentState.Update();
   }
}

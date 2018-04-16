using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.GameLogic;
using NUnit.Framework;
using UnityEditor.TestTools;
using UnityEngine.SceneManagement;

public class GameStateMachineTest
{
    private GameStateMachine machine;

    [SetUp]
    public void SetUp()
    {
        machine = new GameStateMachine();
    }

    [Test]
    public void ShouldStartOnLaunchState()
    {
        Assert.AreEqual(GameStateMachine.GameState.Launch, machine.State);
    }

    [Test]
    public void ShouldTogglePauseStateBackAndForth()
    {
        Assert.AreEqual(GameStateMachine.GameState.Launch, machine.State);

        machine.TogglePause(true);

        Assert.AreEqual(GameStateMachine.GameState.Paused, machine.State);

        machine.TogglePause(false);

        Assert.AreEqual(GameStateMachine.GameState.Launch, machine.State);
    }

    [Test]
    public void ShouldChangeToFlightState()
    {
        machine.ToFlight();

        Assert.AreEqual(GameStateMachine.GameState.Flight, machine.State);
    }

    [Test]
    public void ShouldChangeToEndState()
    {
        machine.ToEnd();

        Assert.AreEqual(GameStateMachine.GameState.End, machine.State);
    }

    [Test]
    public void ShouldNotChangeToFlightStateDuringPause()
    {
        machine.TogglePause(true);
        machine.ToFlight();

        Assert.AreNotEqual(GameStateMachine.GameState.Flight, machine.State);
    }

    [Test]
    public void ShouldNotChangeToFlightStateAfterGameOver()
    {
        machine.ToEnd();
        machine.ToFlight();

        Assert.AreNotEqual(GameStateMachine.GameState.Flight, machine.State);
    }

    [Test]
    public void ShouldNotChangeToEndStateDuringPause()
    {
        machine.TogglePause(true);
        machine.ToEnd();

        Assert.AreNotEqual(GameStateMachine.GameState.End, machine.State);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Source.GameLogic;
using Assets.Source.Repositories;
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
        Assert.AreEqual(GameState.Launch, machine.State);
    }

    [Test]
    public void ShouldTogglePauseStateBackAndForth()
    {
        Assert.AreEqual(GameState.Launch, machine.State);

        machine.TogglePause(true);

        Assert.AreEqual(GameState.Paused, machine.State);

        machine.TogglePause(false);

        Assert.AreEqual(GameState.Launch, machine.State);
    }

    [Test]
    public void ShouldChangeToFlightState()
    {
        machine.ToFlight();

        Assert.AreEqual(GameState.Flight, machine.State);
    }

    [Test]
    public void ShouldChangeToEndState()
    {
        machine.ToEnd();

        Assert.AreEqual(GameState.End, machine.State);
    }

    [Test]
    public void ShouldNotChangeToFlightStateDuringPause()
    {
        machine.TogglePause(true);
        machine.ToFlight();

        Assert.AreNotEqual(GameState.Flight, machine.State);
    }

    [Test]
    public void ShouldNotChangeToFlightStateAfterGameOver()
    {
        machine.ToEnd();
        machine.ToFlight();

        Assert.AreNotEqual(GameState.Flight, machine.State);
    }

    [Test]
    public void ShouldNotChangeToEndStateDuringPause()
    {
        machine.TogglePause(true);
        machine.ToEnd();

        Assert.AreNotEqual(GameState.End, machine.State);
    }
}

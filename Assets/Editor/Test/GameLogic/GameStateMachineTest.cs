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
    private GameStateRepository repo = new GameStateRepository();
    private GameStateMachine machine;

    [SetUp]
    public void SetUp()
    {
        machine = new GameStateMachine(GameState.Launch, repo);
    }

    [Test]
    public void ShouldStartOnLaunchState()
    {
        Assert.AreEqual(GameState.Launch, repo.State);
    }

    [Test]
    public void ShouldTogglePauseStateBackAndForth()
    {
        Assert.AreEqual(GameState.Launch, repo.State);

        machine.TogglePause();
        Assert.AreEqual(GameState.Paused, repo.State);

        machine.TogglePause();
        Assert.AreEqual(GameState.Launch, repo.State);
    }

    [Test]
    public void ShouldChangeToFlightState()
    {
        machine.ToFlight();

        Assert.AreEqual(GameState.Flight, repo.State);
    }

    [Test]
    public void ShouldChangeToEndState()
    {
        machine.ToEnd();

        Assert.AreEqual(GameState.End, repo.State);
    }

    [Test]
    public void ShouldNotChangeToFlightStateDuringPause()
    {
        machine.TogglePause();
        machine.ToFlight();

        Assert.AreNotEqual(GameState.Flight, repo.State);
    }

    [Test]
    public void ShouldNotChangeToFlightStateAfterGameOver()
    {
        machine.ToEnd();
        machine.ToFlight();

        Assert.AreNotEqual(GameState.Flight, repo.State);
    }

    [Test]
    public void ShouldNotChangeToEndStateDuringPause()
    {
        machine.TogglePause();
        machine.ToEnd();

        Assert.AreNotEqual(GameState.End, repo.State);
    }
}

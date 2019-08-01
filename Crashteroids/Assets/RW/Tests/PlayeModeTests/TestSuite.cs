using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuites
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        // Use "Resources/Prefabs/Game" to create an instance of the "Game(GameObject)".
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));

        // Get "Game(Script)" as a component of "Game(GameObject)".
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }
    
    // This is an attribute. Attributes define special compiler behaviors. 
    // It tells the Unity compiler that this is a unit test.
    // This will make it appear in the Test Runner when you run your tests.
    [UnityTest]
    // Test the asteroids actually move down.
    public IEnumerator AsteroidsMoveDown()
    {
        /* // Use "Resources/Prefabs/Game" to create an instance of the "Game(GameObject)".
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));

        // Get "Game(Script)" as a component of "Game(GameObject)".
        game = gameGameObject.GetComponent<Game>(); */

        // Get "Spawner(Script)" as a component of "Spawner(Gamebject)" in "Game(Script)" of "Game(GameObject)".
        // Use SpawnAsteroid() in Spawn class to create an astroid.
        // The astroid has Move method and be called in Update method.
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        // Keep track of the initial position.
        float initialYPos = asteroid.transform.position.y;

        // Add a time-step of 0.1 seconds.
        yield return new WaitForSeconds(0.1f);

        // This is the assertion step where you are asserting
        // that the position of the asteroid is less than the initial position (which means it moved down).
        Assert.Less(asteroid.transform.position.y, initialYPos);

        /* // It’s always critical that you clean up (delete or reset) your code after a unit test so that when the next test runs there are no artifacts that can affect that test.
        // Deleting the game object is all you have left to do, since for each test you’re creating a whole new game instance for the next test.
        Object.Destroy(game.gameObject); */
    }

    
    [UnityTest]
    // Test game over when the ship crashes into an asteroid.
    public IEnumerator GameOverOccurOnAsteroidCollision()
    {
        /* // Use "Resources/Prefabs/Game" to create an instance of the "Game(GameObject)".
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));

        // Get "Game(Script)" as a component of "Game(GameObject)".
        game = gameGameObject.GetComponent<Game>(); */

        // Get "Spawner(Script)" as a component of "Spawner(Gamebject)" in "Game(Script)" of "Game(GameObject)".
        // Use SpawnAsteroid() in Spawn class to create an astroid.
        // The astroid has Move method and be called in Update method.
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        // Set the asteroid to have the same position as the ship to make an asteroid and ship crash
        asteroid.transform.position = game.GetShip().transform.position;

        // Add a time-step to ensure the Physics engine Collision event
        yield return new WaitForSeconds(0.1f);

        // Check that the isGameOver flag in the Game script has been set to true
        Assert.True(game.isGameOver);

       /*  // Delete the "game(GameObject)"
        Object.Destroy(game.gameObject); */
    }

    [UnityTest]
    // Test when the player clicks New Game that the gameOver bool is not true
    public IEnumerator NewGameRestartGame()
    {
        // Set the isGameOver to true
        game.isGameOver = true;
        // NewGame() will set this flag back to false.
        game.NewGame();

        // Assert that the isGameOver bool is false, which should be the case after a new game is called
        Assert.False(game.isGameOver);

        yield return null;
    }

    [UnityTest]
    // Test Lasers Destroy Asteroids
    public IEnumerator LaserDestroyAsteroid()
    {
        // create an asteroid
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // set the asteroid position
        asteroid.transform.position = Vector3.zero;

        // create a laser
        GameObject laser = game.GetShip().SpawnLaser();
        // set the laser position same as the asteroid
        laser.transform.position = Vector3.zero;

        // Add a time-step
        yield return new WaitForSeconds(0.1f);

        // Unity has a special Null class which is different from a “normal” Null class. 
        // The NUnit framework assertion Assert.IsNull() will not work for Unity null checks.
        // When checking for nulls in Unity, you must explicitly use the UnityEngine.Assertions.Assert, not the NUnit Assert.
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    // Test that destroying asteroids rises the score
    public IEnumerator DestroyedAsteroidsRaisesScore()
    {
        // create an asteroid
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // set the asteroid position
        asteroid.transform.position = Vector3.zero;

        // create a laser
        GameObject laser = game.GetShip().SpawnLaser();
        // set the laser position same as the asteroid
        laser.transform.position = Vector3.zero;

        // Add a time-step
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 1);
    }
}
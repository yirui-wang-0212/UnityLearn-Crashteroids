using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuites
{
    private Game game;
    
    // This is an attribute. Attributes define special compiler behaviors. 
    // It tells the Unity compiler that this is a unit test.
    // This will make it appear in the Test Runner when you run your tests.
    [UnityTest]
    // Test the asteroids actually move down.
    public IEnumerator AsteroidsMoveDown()
    {
        // Use "Resources/Prefabs/Game" to create an instance of the "Game(GameObject)".
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));

        // Get "Game(Script)" as a component of "Game(GameObject)".
        game = gameGameObject.GetComponent<Game>();

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

        // It’s always critical that you clean up (delete or reset) your code after a unit test so that when the next test runs there are no artifacts that can affect that test.
        // Deleting the game object is all you have left to do, since for each test you’re creating a whole new game instance for the next test.
        Object.Destroy(game.gameObject);
    }
}
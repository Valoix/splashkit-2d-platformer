// Project 1: Barebones 2D Platformer
// Daniel Hurst
// 08/04/2024

using System;
using System.Security.Cryptography.X509Certificates;
using SplashKitSDK;

namespace projectone
{
    public class Program
    {
        public static void Main()
        {
            // Confirm that it ran successfully.
            Console.WriteLine("Barebones 2D Platformer");
            Console.WriteLine("Running...");

            // CONSTANTS
            const int SCREEN_HEIGHT = 720;
            const int SCREEN_WIDTH = 1280;

            const int PLAYER_HEIGHT = 200;
            const int PLAYER_WIDTH = 100;
            const int PLAYER_SPEED = 16;

            // VARIABLES
            int playerX = SCREEN_WIDTH / 2;
            int playerY = SCREEN_HEIGHT / 2;
            double playerVelocity = 0;
            double playerGravity = 0;
            bool playerIsGrounded = false;

            bool windowIsRunning = true;

            void DrawPlayer(){
                SplashKit.FillEllipse(Color.Red, playerX - PLAYER_WIDTH/2, playerY - PLAYER_HEIGHT/2, PLAYER_WIDTH, PLAYER_HEIGHT);
            }

            // Initialise the window
            SplashKit.OpenWindow("Barebones 2D Platformer", SCREEN_WIDTH, SCREEN_HEIGHT);

            while (windowIsRunning) {
                
                // Check if ESC is pressed --> Close game
                if (SplashKit.KeyDown(KeyCode.EscapeKey)) {
                    windowIsRunning = false;
                }

                // Move the player
                if (SplashKit.KeyDown(KeyCode.LeftKey) && playerX - PLAYER_WIDTH > 0) { // Left key pressed --> Move left
                    if (SplashKit.KeyDown(KeyCode.LeftShiftKey)){ // Shift key pressed --> Sprint
                        playerVelocity = PLAYER_SPEED * -2;
                    } else{
                        playerVelocity = PLAYER_SPEED * -1;
                    }
                    
                } if (SplashKit.KeyDown(KeyCode.RightKey) && playerX + PLAYER_WIDTH < SCREEN_WIDTH) { // Right Key pressed --> Move right
                    if (SplashKit.KeyDown(KeyCode.LeftShiftKey)){ // Shift key pressed --> Sprint
                        playerVelocity = PLAYER_SPEED * 2;
                    } else{
                        playerVelocity = PLAYER_SPEED * 1;
                    }
                } else {
                    if (playerVelocity > 0) { // Return to resting position if moving right
                        playerVelocity -= 2;
                    } else if (playerVelocity < 0) { // Return to resting position if moving left
                        playerVelocity += 2;
                    }
                }
                playerX += (int)playerVelocity;

                // Jumping
                if (SplashKit.KeyDown(KeyCode.SpaceKey) && playerIsGrounded) {
                    playerY -= 10;
                    playerGravity = -60;
                    playerIsGrounded = false;
                } 
                if (playerY + PLAYER_HEIGHT/2 < SCREEN_HEIGHT-100) {
                    playerGravity += 5;
                } else{
                    playerGravity = 0;
                    playerIsGrounded = true;
                    if (playerY + PLAYER_HEIGHT/2 > SCREEN_HEIGHT-100){
                        playerY = SCREEN_HEIGHT - 200;
                    }
                }
                playerY += (int)playerGravity;

                // Wipe the screen ready for refresh
                SplashKit.FillRectangle(Color.White, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

                // Create the player
                DrawPlayer();

                // Create the floor
                SplashKit.FillRectangle(Color.Black, 0, SCREEN_HEIGHT - 100, SCREEN_WIDTH, 100);

                // Screen Refresh
                
                SplashKit.RefreshScreen(60);
                SplashKit.Delay(100);

            }
        }
    }
}

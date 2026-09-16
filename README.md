# Sportsmen Classification App

A Unity app that classifies a football player as **attacking** or **defensive** from their test results, using linear discriminant analysis.

## How it works

- The training data is two groups of real players with six FIFA-style ratings each: `Assets/Raw/Class1.txt` (attacking) and `Assets/Raw/Class2.txt` (defensive).
- The app computes each group's mean vector and the covariance matrix, then builds a linear discriminant function.
- You enter a new player's name and 12 test results. The app turns them into the same six ratings and the sign of the discriminant decides the group.
- Players can be added to either group, the database can be browsed, and cross-validation checks the model.
- After the result, the app offers training videos for that player type in a web view.

## Tech

- Unity, C#
- [unity-webview](https://github.com/gree/unity-webview) for the in-app web view
- iOS build exported to Xcode (`SportsmenClassification/`)

## Run

Open the project in Unity and start the `StartScreen` scene.

Built in 2020.

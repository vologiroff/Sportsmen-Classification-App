# Sportsmen Classification App

A Unity app that learns from two groups of football players, forwards and midfielders, and predicts which group a new player belongs to.

## How it works

- Each player is described by six FIFA-style ratings: pace, shooting, passing, dribbling, defending and physical.
- The training data lives in `Assets/Raw/Class1.txt` (forwards) and `Assets/Raw/Class2.txt` (midfielders).
- The classifier is statistical: it computes the mean vector and covariance matrix of each group and assigns a new player to the closer group.
- The app lets you enter a player's ratings, run the prediction, validate the model and manage the player database.

## Tech

- Unity, C#
- [unity-webview](https://github.com/gree/unity-webview) for the in-app web view
- iOS build exported to Xcode (`SportsmenClassification/`)

## Run

Open the project in Unity and start the `startScreen` scene.

Built in 2020.

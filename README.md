# Kitten Player

A foobar2000 clone, integrating usability of common player with youtube-dl features. Saves offline and organizes tracks from Youtube.

Alpha version soon for Windows platform.

## Pictures

![](/pics/30.10.17.png)

## Features implementation

### Undo-Redo Feature

Probably one of the most irritiating feature to implement. I finally decided to use list of Action delegates in separated class.
User is able to:

+ [-] Add tracks
+ [-] Remove tracks
+ [-] Rearrange tracks
+ [-] Rename tracks
+ [-] Add group of tracks
+ [-] Remove group of tracks
+ [-] Move group of tracks
+ [-] Sort group of tracks
+ [-] Add playlist
+ [-] Remove playlist
+ [-] Rename playlist
+ [-] Move playlist

### Youtube functionality

User will be able to:

+ [+] download specific track as mp3
+ [+] get a list of tracks found with specific search title
+ [+] load track names and links from playlist link
+ [+] set whole youtube playlist as a playlist tab elements
+ [-] get a list of playlists found with specific search title
+ [-] get a playlist associated with specific track

### Music Player

I use NAudio library.

## Platforms

For now player is developed for Windows only, I have in mind to make it possible to be natively compiled on Linux.

## License

Full open-source-viral license, GPLv3.0.
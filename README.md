# Kitten Player

A foobar2000 clone, integrating usability of common player with youtube-dl features. Saves offline and organizes tracks from Youtube.

## User stories

For now the following features have been planed (+ for already implemented, - for not yet implemented). 

### Player functionality

User can:

+ [+] drag-and-drop list of files to playlist
+ [+] add a new playlist
+ [+] rename a playlist
+ [+] delete a playlist
+ [+] play selected track
+ [+] pause playing current track
+ [+] stop playing current track
+ [+] use Play/Pause/Stop buttons to control the track play
+ [+] have tracks as custom names without renaming original files
+ [+] rearrange playlist tabs
+ [+] delete a group of tracks from the playlist
+ [+] set specific location for drag-and-drop operation
+ [+] move tracks on a playlist
+ [+] get playlists saved in AppData files
+ [+] get playlists loaded from AppData files
+ [+] adjust the volume of the program using a volume bar
+ [+] change current track time on a trackbar
+ [+] see current track time on a trackbar
+ [+] automatically play next track
+ [+] have track properties in playlist tabs
+ [+] open the menu list and add files to playlist tab
+ [+] add the YouTube playlist to playlist tab
+ [+] add whole directory to playlist
+ [+] change track properties
+ [+] rearrange display of track properties
+ [-] undo and redo any action

### Youtube functionality

User will be able to:

+ [+] download specific track as mp3
+ [+] get a list of tracks found with specific search title
+ [+] load track names and links from playlist link
+ [+] set whole youtube playlist as a playlist tab elements
+ [-] get a list of playlists found with specific search title
+ [-] get a playlist associated with specific track

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

### Music Player

I use NAudio library.

## Class diagram

![Class diagram](/ClassDiagram.png)

## Platforms

For now player is developed for Windows only, I have in mind to make it possible to be natively compiled on Linux.

## License

Full open-source-viral license, GPLv3.
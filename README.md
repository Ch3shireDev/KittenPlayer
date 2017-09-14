# KittenPlayer

A minimalistic player, foobar2000 clone. Apart from the obvious player features program will also implement youtube-dl usability.

## User stories

For now the following features have been planed (+ for already implemented, - for not yet implemented). 

### Player functionality

User can:

+ [+] drag-and-drop list of files to playlist
+ [+] add a new playlist
+ [+] rename a playlist
+ [+] delete a playlist
+ [+] use Play/Pause/Stop buttons to control the track play
+ [+] have tracks as custom names without renaming original files
+ [+] undo and redo any action
+ [+] set specific location for drag-and-drop operation
+ [+] move tracks on a playlist
+ [+] rearrange playlist tabs
+ [+] play selected track by pressing Play button
+ [+] play selected track by LMB double-click
+ [+] play selected track by pressing Enter
+ [+] play selected track by choosing an option from drop-down menu (from clicking RMB)
+ [+] pause playing current track (by pressing button, Space, drop-down menu)
+ [+] stop playing current track (by pressing button and from drop-down menu)
+ [+] automatically play next track
+ [-] get playlists saved in AppData files
+ [-] get playlists loaded from AppData files
+ [-] delete a track from the playlist
+ [-] drop a list of files as a new tab
+ [-] have track properties in playlist tabs
+ [-] rearrange display of track properties
+ [-] sort files by names, respective to folders
+ [-] open the menu list and add files to playlist
+ [-] open the menu list and add whole folder to playlist

### Youtube functionality

User will be able to:

+ [-] get a list of tracks found with specific search title
+ [-] get a list of playlists found with specific search title
+ [-] get a playlist associated with specific track
+ [-] load track names and links from playlist link
+ [-] download specific track as mp3

## Features implementation

### Undo-Redo Feature

Probably one of the most irritiating feature to implement. I finally decided to use list of Action delegates in separated class.
User is able to:

+ [+] Add tracks
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

For now I removed WMPLib from MusicPlayer class - sadly it generated bugs during development.

## Class diagram

![Class diagram](/ClassDiagram.png)

## Platforms

For now player is developed for Windows only, I have in mind to make it possible to be natively compiled on Linux.

## License

Full open-source-viral license, GPLv3.
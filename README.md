# KittehPlayer

A minimalistic player, foobar2000 clone. Apart from the obvious player features program will also implement youtube-dl usability.

## User stories

For now the following features have been planed (+ for already implemented, - for not yet implemented). 

### Player functionality

User can:

+ [+] drag-and-drop list of files to playlist
+ [+] add a new playlist
+ [+] rename a playlist
+ [+] delete a playlist
+ [+] double click a position on playlist to begin playing
+ [+] pause playing current track
+ [+] stop playing current track
+ [+] use Play/Pause/Stop buttons to control the track play
+ [+] have tracks as custom names without renaming original files
+ [-] undo and redo any action
+ [-] rearrange playlist tabs
+ [-] delete a track from the playlist
+ [-] move single track on a playlist
+ [-] move many tacks on a playlist
+ [-] have track properties in playlist tabs
+ [-] rearrange display of track properties
+ [-] automatically play next track
+ [-] sort files by names, respective to folders
+ [-] open the menu list and add files to playlist
+ [-] open the menu list and add whole folder to playlist
+ [-] get playlists saved in AppData files
+ [-] get playlists loaded from AppData files

### Youtube functionality

User will be able to:

+ [-] get a list of tracks found with specific search title
+ [-] get a list of playlists found with specific search title
+ [-] get a playlist associated with specific track
+ [-] load track names and links from playlist link
+ [-] download specific track as mp3

## Class diagram

![Class diagram](/ClassDiagram.png)

## Platforms

For now player is developed for Windows only, I have in mind to make it possible to be natively compiled on Linux.

## License

Full open-source-viral license, GPL3.
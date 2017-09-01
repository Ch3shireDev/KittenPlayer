# KittehPlayer

A minimalistic player, foobar2000 clone. Apart from the obvious player features program will also implement youtube-dl usability.

## User stories

For now the following features have been planed (+ for already implemented, - for not yet implemented). User can:

+ [+] drag-and-drop list of files to playlist
+ [+] add a new playlist
+ [+] rename a playlist
+ [+] delete a playlist
+ [-] rearrange playlist tabs
+ [-] remove a position from the playlist
+ [+] double click a position on playlist to begin playing
+ [-] pause playing current track
+ [-] stop playing current track
+ [-] sort files by names, respective to folders
+ [-] open the menu list and add whole folder to playlist
+ [-] use Play/Pause/Stop buttons to control the track play
+ [+] have tracks as custom names without renaming original files
+ [+] get playlists save in AppData files
+ [+] get playlists load from AppData files (bugs)

### Youtube functionality

User will be able to:

+ [-] get a list of videos found with specific search title
+ [-] load track names and links from playlist link
+ [-] download specific track as mp3

## Class diagram

![Class diagram](/ClassDiagram.png)

## Platforms

For now player is developed for Windows only, I have in mind to make it possible to be natively compiled on Linux.

## Licence

Full viral-open-source licence, GPL3.
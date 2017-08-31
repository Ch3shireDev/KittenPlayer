# KittehPlayer

A minimalistic player, foobar2000 clone. Idea is to give user the ability to create and save playlists for files on a PC, as well as being able to automatically search and download music playlists from YouTube. 

## User stories

For now the following features have been planed (+ for already implemented, - for not yet implemented):

+ [+] User can drag-and-drop list of files to playlist
+ [+] User can add a new playlist
+ [+] User can rename a playlist
+ [-] User can delete a playlist
+ [-] User can remove a position from the playlist
+ [+] User can double click a position on playlist to begin playing
+ [-] User can pause playing current track
+ [-] User can stop playing current track
+ [-] User can sort files by names, respective to folders
+ [-] User can open the menu list and add whole folder to playlist
+ [-] User has a Play/Pause/Stop buttons to control the track play
+ [-] Tracks are just filenames and can be renamed without change on actual files
+ [-] Playlists are automatically saved and load in Appdata/Local

## Class diagram

![Class diagram](/ClassDiagram.png)

## Platforms

For now player is developed for Windows only, I have in mind to make it possible to be natively compiled on Linux.
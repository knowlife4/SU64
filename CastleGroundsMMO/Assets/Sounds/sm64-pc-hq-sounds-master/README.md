# sm64-pc-hq-sounds
This is a project aiming to bring high quality sounds to Super Mario 64 for PC

Currently, this mod only works on EX branch of the port, but can be built with other forks. PAK versions will be coming once I have the time.

NOTICE: If you're coming from the channel Unreal, go to this video for a non-crappy comparison video. That guy seriously bitcrunched his audio to make it even harder to differentiate.
https://www.youtube.com/watch?v=zDH45ip2TUw

### Credits


<pre>Map                      Rough Stone Sliding, Switch, recreated Lava, Waterfall 2, Metalcap, Tabla 3
Aqua                     Bat, Pat, French Horn
armageddon13             Crystal Rhodes
Desko                    Whoosh, Mario "Yahoo!"
Liam                     Pah Voice
Infomanic95              Convolution, Metal Rimshot, Kick Drum 2
michael02022             Lahna, Orchestral Hit
VGM Sources Discord      Most of the instruments
Sound Effects Wiki       Most of the sounds
Uncredited Person        Thwomp/Whomp</pre>


### Sources

<pre>Super Smash Bros. Melee                     Most of the mario voices
Super Mario 64 OST                          "It's-a me, Mario!"
Super Mario World                           Yoshi
Nintendo Puzzle Collection                  Mario "Yahoo!"
Roland SC-88                                Strings 2, Music Box, Glockenspiel, French Horn
Roland SC-55                                Pat
Roland JD-990                               Crystal Rhodes, Pan Flute
KORG 01W/R                                  Woo Voice
Sonic Images Library Vol. 1                 Monk Choir, Tambourine, Steel Drum
Sound Ideas Series 6000                     A good chunk of the sounds
Voice Spectral Vol. 1                       Yelp (Unused), Pah Voice
BFA Megaton Pack                            Tabla 3
A Poke in the Ear with a Sharp Stick        Metal Rimshot, Kick Drum 2, Convolution, Lahna
Hollywood Premiere Edition Vol. 1           Big Splash, Ukiki
Warner Bros. Sound Effect Library           A good chunk of the sounds
Universal Studios Sound Effect Library      Bowser Roar, Boing, Boing (Unused), Box Break</pre>


### Installation/Building

To use this mod, download from the release section and make sure your EX build has external resources enabled. Put it in your res folder (still zipped) and launch the game. If you encounter any audio clipping, lower your sfx volume. If the mod did not work out for any reason, continue reading.

To build this in with non-EX branches, or if you want to make changes to the mod, grab the contents of "Sounds for building" and put them in your builder's sound/samples folder (Not the one inside your build folder, if you see aifc's you're in the wrong place. If there isn't a samples folder, make one). 

If you plan on sharing it, make sure you have external resources turned on so you can seperate the base content. Once done building, go to your res folder and open base.zip. In your sounds folder there should be 2 sound_data files. Grab them and zip them up with the file structure they were in, leaving them the only files in the whole pack.


### Notes

The "Sounds for building" has certain differences with the "HQ Sounds" folder, as they are converted to mono, don't have every sound, and some are sampled lower to compensate for the audio engine. "HQ Sounds" is for future builds that support a more robust audio system.

Refer to the Sound Sheet to know what sounds have a source or are upsampled, as well as the complete listing of every sound in the game. To get the vanilla sounds from the game, go to your sound/samples folder. If there is no folder, build the game (You do not have to wait till it's finished building, they are extracted at the start of the process)

The sound-auditions branch is for anyone who wants to submit sounds that are close to the original sounds. I'm more liberal with sound effects, but instruments have to be exact. Submit pull requests there.


### Stats

<pre>Catagorized:          212/217 (97.6%)
Sources:              82/217  (37.8%)
Upsampled:            22/217  (10.1%)
Sources + Upsampled:  7/217   (3.2%)
Questionable          10/217   (4.6%)

Sounds:               97/137  (70.8%)
Instruments:          24/80   (30%)
Total:                121/217 (55.8%)</pre>


### Future

I don't anticipate this project to get very far until Liam releases his own sound mod, supposedly with more samples and a remastered soundtrack. I'll leave it to him to do what he feels right, but in the mean time this will stay up regardless.

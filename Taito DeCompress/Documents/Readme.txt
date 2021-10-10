TAITO Compress is a tool for decompressing, compressing and reinserting data in some SNES/SFC TAITO games like
- Arkanoid: Doh It Again
- Bust-A-Move / Puzzle Bobble
- Maybe some others, too ...

Hacking:	Svambo
Editor:		Dom aka Doke

Usage:
------
Deompressing data:
1. Insert offset in hex of the object you want to decompress
2. Click button "Decompress"
3. Select the ROM file
4. Done! Your decompressed data object is stored in ROM directory
5. Now you can go on editing this file f.e. with a tile editor

Compressing data:
1. Click button "Compress file"
2. Select your edited DEC file
3. Done! Your decompressed data object is stored in ROM directory

Reinserting data:
1. Insert offset in hex of the object you want to reinsert
2. Click button "ROM"
3. Select the ROM file you want to insert to
4. Click button "CMP"
5. Select your edited and compressed CMP file
6. Click button "Insert"
7. Done! Your edited and compressed data object is inserted into your ROM file

Notes:
------
- If you are having problems in decompressing data, try to subtract 2 bytes from your offset (first 2 bytes from compressed object are storing size information)
- Never change filename of decompressed data (just don't do it ... )
- If it tells you, that the data object you want to insert is too large, it's your turn to do some manual stuff
- This tool is not you Momma, so you alway should know what you're doing
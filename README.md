# VIN Validator and Decoder
Validates and decodes vehicle identification numbers (VIN). 

## Validator



#### Length
Starting with 1981, a valid VIN must be 17 characters in length. 

#### Illegal Characters
A valid VIN cannot contains letter O, Q, and I to avoid confusion with number 1 and 0 (zero).

#### VIN Checksum Algorithm
Vehicles sold in the US and Canada use 9th digit in checksum test to ensure the VIN is valid. 

##### Step 1: 
**Transliteration**

Every letter is converted to a number using the following matrix: 
 
| Letter | Value | Letter | Value |
|:-: |:-:|:-:|:-:|
|A|1|M|4|
|B|2|N|5|
|C|3|P|7|
|D|4|R|9|
|E|5|S|3|
|F|6|T|4|
|G|7|U|4|
|H|8|V|5|
|J|1|W|6|
|K|2|X|7|
|L|3|Y|8|
| | |Z|9|

##### Step 2: 
**Computing weighted product**


##### Step 3: 

##### Step 4: 


##### SQL Server Function
[This SQL function](https://gist.github.com/dvysotskiy/2fb90e6bda0f2feac0c04243d7751ca5) performs VIN validation described above.


## Decoder

*Requires Sqlite database*

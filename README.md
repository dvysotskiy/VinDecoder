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
Compute a **sum** of products of each position by multiplying transliterated VIN character by a weighted factor.

|Position| 1  |  2 |  3 | 4  | 5  | 6  |  7 | 8 | 9 | 10  |  11 |  12 | 13 | 14 | 15 | 16 | 17 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
|Weight | 8  | 7  | 6  | 5  | 4  | 3  | 2  | 10  | 0 | 9  | 8  | 7  | 6  | 5  |  4 | 3  |  2 |


##### Step 3: 
Find the remainder of the weighted sum divided by 11. If the remainder is between 1 and 9 inclusively, it should match the check digit in the 9th position of the VIN. If the remainder is 10, then the check digit must be 'X' for the VIN to be valid.



#### SQL Server Function
[This SQL function](https://gist.github.com/dvysotskiy/2fb90e6bda0f2feac0c04243d7751ca5) performs VIN validation described above.


## Decoder

##### WMI:
The first three characters of the VIN uniquely identify the vehicle manufacturer. The first character of the VIN identifies the region in which the manufacturer is located. 9 in the 3rd position indicate that the manufacturer builds fewer than 1,000 vehicles per year.




*Supports 9,017 WMIs*


##### Year:

If the 7th character is numeric, the model year is between 1981 and 2009; otherwise, 2010 and later. In addition to restricted characters, U, Z and 0 (zero) are not permitted in 10th position used to determine vehicle year.

|Year| 2003 |  2004 |  2005 | 2006 | 2007 | 2008 |  2009 | 2010 | 2011 | 2012  |  2013 |  2014 | 2015 | 2016 | 2017 | 2018 | 2019 |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
|Before 2010 | Yes  | Yes  | Yes  | Yes  | Yes  | Yes  | Yes  | No  | No | No  | No  | No  | No  | No  |  No | No  |  No |
|Code | 3  | 4  | 5  | 6  | 7  | 8  | 9  | A  | B | C  | D  | E  | F  | G  | H | J  | K |

*Supported year range is 1980 - 2039*


##### Serial Number:
Sequential or serial number assigned by the manufacturer is stored in position 12 - 17. Small manufacturers producing less than 500 vehicles annually can be identified by the characters in position 12 - 14.



***Requires Sqlite database***

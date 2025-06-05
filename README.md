# parking_system

## Example Usage
```bash
$ create_parking_lot 6
Created a parking lot with 6 slots

$ park B-1234-XYZ Putih Mobil
Allocated slot number: 1

$ park B-9999-XYZ Putih Motor
Allocated slot number: 2

$ status
Slot No.    Type    Registration No Colour
1       B-1234-XYZ      Mobil   Putih
2       B-9999-XYZ      Motor   Putih

$ type_of_vehicles Mobil
1

$ leave 1
Slot number 1 is free

$ registration_numbers_for_vehicles_with_colour Putih
B-9999-XYZ

$ exit
```
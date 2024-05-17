# SettlementService


 Requiremnt : accepts up to 4 simultaneous settlements was a bit confusing
 The requirement seems unclear. Assuming that the service should accept up to 4 bookings at the same time.
 As in if the time is passed to make a booking, with that hour, the service should accept up to 4 bookings.

 https://localhost:7200/swagger/index.html

 Uses swagger UI

 



 Technical requirements 
• Assume that all bookings are for the same day (do not worry about handling dates)
 • hours of business are 9am-5pm, all bookings must complete by 5pm (latest booking
 is 4:00pm)
 • Bookings are for 1 hour (booking at 9:00am means the spot is held from 9:00am to 9:59am)
 • accepts up to 4 simultaneous settlements
 • API needs to accept POST requests of the following format:
 {
 "bookingTime": "09:30", 
"name":"John Smith" 
} 
• Successful bookings should respond with an OK status and a booking Id in GUID form
 {
 "bookingId": "d90f8c55-90a5-4537-a99d-c68242a6012b" 
} 
• Requests for out of hours times should return a Bad Request status
 • Requests with invalid data should return a Bad Request status
 • Requests when all settlements at a booking time are reserved should return a Conflict status
 • The name property should be a non-empty string
 • The bookingTime property should be a 24-hour time (00:00 - 23:59)
 • Bookings can be stored in-memory, it is fine for them to be forgotten when the application is
 restarted

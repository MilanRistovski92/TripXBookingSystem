# TripXBookingSystem

## Description
TripXBookingSystem is mock booking system developed for managing bookings and reservations for hotels, flights, and other travel services. The API supports operations such as hotel/flight search, booking creation, and status checking.
The API is designed to follow the provided flow in the task (Search -> Book -> CheckStatus), so in order to get results via Postman/Swagger when testing, you need to follow the flow (first get the options, then provide the code along with other information in the booking endpoint, and then use the response from the booking in the checkstatus endpoint)

### Prerequisites
- .NET 6.0 SDK
- Visual Studio 2022 or similar IDE with C# support

## Usage
- Before utilizing the search, book and checkstatus endpoints, please make a call for the Auth endpoint, and copy the token in order to be able to use the endpoints and get data. Note: you don't need to use 'Bearer' before the token.
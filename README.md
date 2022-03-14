![image](https://user-images.githubusercontent.com/100145705/158087777-5e994db8-7fda-4453-839a-fa66f8a24b63.png)


# Carpooling

Carpooling by Bojidar Dimitrov and Anton Vasilkovski

Carpooling is a web application that enables you to share your travel from one location to other with other passengers. Every user can either organize a shared travel or request to join someone else’s travel.

# Swagger documentation

On localhost: http://localhost:5000/swagger/index.html

## Features

- Public Part

Registration and login.

List of the top 10 travel organizers and top 10 passengers.

- Private part

Users can login/logout, update their profile, set a profile photo/avatar.

Create a new travel.

Browse the available trips.

Apply for a trip as a passenger. 

Cancel a trip before the departure time.

Mark the trip as complete.

Leave a comment about driver/passenger.

View all his travels.

- Administrative part

Accessible to admins.

See a list of all users and search them.

See a list of all travels and search them.


## Installation

1. Clone/Download the project from the Gitlab repository.

2. In the appsettings.json setup the connection string.

3. Use the NuGet package manager console located in:
Tools > NuGet Package Manager > Package Manager Console

4. Add Initial migration through the console
```bash
  Add-migration Initial
```

5. Update the Database through the console
```bash
  Update-Database
```

6. You are ready to start the aplication and use it.

## Database Relations

![image](https://user-images.githubusercontent.com/100145705/158087745-178dc35d-ce04-48de-a94a-8e090e5ce3e4.png)

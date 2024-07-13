# Hotel reservation system with clean architecture and DDD patterns

## Introduction

This project serves as a sample implementation of a hotel reservation system using .NET Core. It demonstrates the application of Clean Architecture, Domain-Driven Design (DDD) principles, and modular monolith communication. The goal is to showcase effective utilization of these patterns in practical scenarios.

## Business Objectives

The business objective of this project is to create a sample system for hotel room reservations and hotel network management. It aims to demonstrate a practical approach to applying the above patterns and practices.

## Event Storming

### Big picture and process level

<img width="1848" alt="Zrzut ekranu 2024-07-12 o 21 41 13" src="https://github.com/user-attachments/assets/770cbf0e-1def-4cf2-8835-fba22aa703e5">

### Design level

<img width="2319" alt="Zrzut ekranu 2024-07-12 o 21 46 34" src="https://github.com/user-attachments/assets/711183a6-fe88-45de-9698-e31592a0fdac">

### Bounded contexts and comunication

<img width="2379" alt="Zrzut ekranu 2024-07-12 o 21 49 39" src="https://github.com/user-attachments/assets/ca527f43-c05d-4403-8c58-e0eca51b005a">

## Bounded context explain

### Hotel Management

The Hotel Management module is a CRUD module responsible for managing hotels, rooms, and available amenities. It allows for the creation, reading, updating, and deletion of hotel records, room details, and amenity information. This module ensures efficient administration of hotel properties and their features.

### Catalog

The Catalog module is responsible for storing hotel data and providing search capabilities. It acts as a read model, aggregating data from hotels and reservations to facilitate efficient information retrieval. This module ensures users can easily search for and access detailed hotel information.

### Booking

The Booking module is responsible for creating and managing the entire room reservation process. It handles booking requests, processes reservations, and ensures smooth coordination of all booking-related activities. This module is essential for managing the lifecycle of room reservations from initiation to completion.

### Availability

The Availability module is a generic module responsible for maintaining and monitoring the availability of specific rooms. It ensures that room availability status is accurately tracked and updated, enabling efficient management of room bookings and preventing double bookings. This module is crucial for keeping the reservation system reliable and up-to-date.

### Pricing
The Pricing module is responsible for accurately calculating the price of selected room configurations. It takes into account various factors such as room type, amenities, duration of stay, and any applicable discounts or promotions. This module ensures that pricing is transparent, fair, and dynamically adjusted based on the selected options.

### Rooms preparation

The Preparation module is responsible for preparing rooms for guests. It manages the status of cleaning, check-in, and check-out processes to ensure rooms are ready for incoming guests. This module tracks and updates the preparation status, ensuring a smooth transition between guests and maintaining high standards of room readiness.

### Offering

The Offering module is an additional module responsible for creating preliminary offers without making room reservations. It allows potential guests to receive detailed offers based on their preferences and requirements, including room configurations, pricing, and available amenities. This module helps in providing prospective guests with all the necessary information before they make a reservation decision.

## Architecture and project structure

```bash
├── Bootstrapper
├── CrossContext
│   ├── Contract
│   │   ├── Availability   
│   │   ├── HotelManagement
│   │   ├── Catalog
├── Modules
│   ├── Availability
│   │   │   ├── Api
│   │   │   ├── Application
│   │   │   ├── Domain
│   │   │   ├── Infrastructure
│   ├── Booking
│   │   │   ├── Api
│   │   │   ├── Application
│   │   │   ├── Domain
│   │   │   ├── Infrastructure
│   ├── Catalog
│   │   │   ├── Api
│   │   │   ├── Core
│   ├── HotelManagement
│   │   │   ├── Api
│   │   │   ├── Core
├── Shared

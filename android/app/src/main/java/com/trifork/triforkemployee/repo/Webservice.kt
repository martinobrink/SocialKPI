package com.trifork.triforkemployee.repo

import com.trifork.triforkemployee.database.Employee
import com.trifork.triforkemployee.database.Event
import retrofit2.http.GET
import retrofit2.http.Path

interface Webservice {

    @GET("/employee/{user}")
    suspend fun getEmployee(@Path("user") userId: String): Employee

    @GET("/employee/")
    suspend fun getEmployees(): List<Employee>

    @GET("/event")
    suspend fun getEvents(): List<Event>

    @GET("/event/{id}")
    suspend fun getEvent(@Path("id") eventId: String): Event
}
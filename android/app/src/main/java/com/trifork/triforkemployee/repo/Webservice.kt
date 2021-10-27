package com.trifork.triforkemployee.repo

import com.trifork.triforkemployee.database.employee.Employee
import retrofit2.http.GET
import retrofit2.http.Path

interface Webservice {

    @GET("/employee/{user}")
    suspend fun getEmployee(@Path("user") userId: String): Employee

    @GET("/employee/")
    suspend fun getEmployees(): List<Employee>
}
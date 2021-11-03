package com.trifork.triforkemployee.repo.employee

import android.util.Log
import com.trifork.triforkemployee.database.Employee
import com.trifork.triforkemployee.repo.Webservice
import retrofit2.HttpException
import javax.inject.Inject

class EmployeeRepository @Inject constructor(
    private val webservice: Webservice
) {
    val TAG = "EmployeeRepository"

    suspend fun getEmployees(): List<Employee> {
        return try {
            webservice.getEmployees()
        } catch (e: HttpException) {
            Log.d(TAG, e.toString())
            listOf(
                Employee("0", "Jørgen", "Heinsen", "jhe")
            )
        }
    }

    suspend fun getEmployee(id: String): Employee {
        return try {
            webservice.getEmployee(id)
        } catch (e: HttpException) {
            Log.d(TAG, e.toString())
            Employee( "0","Jørgen", "Heinsen", "jhe")
        }
    }

}
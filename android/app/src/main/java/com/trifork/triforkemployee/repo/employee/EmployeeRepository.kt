package com.trifork.triforkemployee.repo.employee

import android.util.Log
import com.trifork.triforkemployee.database.employee.Employee
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
        }
        catch (e: HttpException) {
            Log.d(TAG, e.toString())
            return listOf()
        }
    }

}
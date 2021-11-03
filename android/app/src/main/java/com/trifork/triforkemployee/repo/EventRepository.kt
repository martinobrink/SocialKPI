package com.trifork.triforkemployee.repo

import android.util.Log
import com.trifork.triforkemployee.database.Event
import retrofit2.HttpException
import javax.inject.Inject

class EventRepository @Inject constructor(
    private val webservice: Webservice
) {
    val TAG = "EventRepository"

    suspend fun getEvents(): List<Event> {
        return try {
            webservice.getEvents()
        } catch (e: HttpException) {
            Log.d(TAG, "getEmployees ${e.message()}")
            listOf(

            )
        }
    }

    suspend fun getEvent(id: String): Event {
        return try {
            webservice.getEvent(id)
        } catch (e: HttpException) {
            Log.d(TAG, "getEvent ${e.message()}")
            Event("", "", "", listOf(), "", "")
        }
    }

}
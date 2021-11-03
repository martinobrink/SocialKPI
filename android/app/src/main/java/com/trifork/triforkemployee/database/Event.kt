package com.trifork.triforkemployee.database

import android.util.Log
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.time.format.DateTimeParseException

class Event(
    val id: String,
    val title: String,
    val category: String,
    val participants: List<Employee>,
    val timeOfEvent: String,
    val createdBy: String
) {

    fun formatTimeOfEvent() : String {
        return try {
            LocalDateTime.parse(timeOfEvent, DateTimeFormatter.ISO_OFFSET_DATE_TIME).format(DateTimeFormatter.ofPattern("HH:mm E d MMM"))
        }catch (e : Exception) {
            Log.d("Event", "formatTimeOfEvent: $e timeOfEvent: $timeOfEvent")
            ""
        }

    }

}
package com.trifork.triforkemployee.ui.events

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.trifork.triforkemployee.database.Employee
import com.trifork.triforkemployee.database.Event
import com.trifork.triforkemployee.repo.EventRepository
import com.trifork.triforkemployee.repo.employee.EmployeeRepository
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class EventsViewModel @Inject constructor(
    eventRepository: EventRepository
) : ViewModel() {

    private val _events = MutableLiveData<List<Event>>()
    val events: LiveData<List<Event>> = _events

    init {
        viewModelScope.launch {
            _events.value = eventRepository.getEvents()
        }
    }
}
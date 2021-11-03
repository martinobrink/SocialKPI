package com.trifork.triforkemployee.ui.employee

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.trifork.triforkemployee.database.Employee
import com.trifork.triforkemployee.repo.employee.EmployeeRepository
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class EmployeeViewModel @Inject constructor(
    private val employeeRepository: EmployeeRepository
) : ViewModel() {

    private val _employee = MutableLiveData<Employee>()
    val employee : LiveData<Employee> = _employee

    fun getEmployee(id: String) {
        viewModelScope.launch {
            _employee.value = employeeRepository.getEmployee(id)
        }
    }
}
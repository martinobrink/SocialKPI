package com.trifork.triforkemployee.ui.employees

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
class EmployeesViewModel @Inject constructor(
    employeeRepository: EmployeeRepository
) : ViewModel() {

    private val _employees = MutableLiveData<List<Employee>>()
    val employees : LiveData<List<Employee>> = _employees

    init {
        viewModelScope.launch {
            _employees.value = employeeRepository.getEmployees()
        }
    }
}
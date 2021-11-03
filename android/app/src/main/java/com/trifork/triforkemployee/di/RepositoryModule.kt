package com.trifork.triforkemployee.di

import com.trifork.triforkemployee.repo.EventRepository
import com.trifork.triforkemployee.repo.Webservice
import com.trifork.triforkemployee.repo.employee.EmployeeRepository
import com.trifork.triforkemployee.ui.employees.EmployeesViewModel
import com.trifork.triforkemployee.ui.events.EventsViewModel
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.components.ActivityComponent

@Module
@InstallIn(ActivityComponent::class)
class RepositoryModule {

    @Provides
    fun providesEmployeeRepository(webservice: Webservice) = EmployeeRepository(webservice)

    @Provides
    fun providesEmployeesViewModel(repository: EmployeeRepository) = EmployeesViewModel(repository)

    @Provides
    fun providesEventsRepository(webservice: Webservice) = EventRepository(webservice)

    @Provides
    fun providesEventsViewModel(repository: EventRepository) = EventsViewModel(repository)
}
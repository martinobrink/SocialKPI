package com.trifork.triforkemployee.di

import com.trifork.triforkemployee.repo.Webservice
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.components.ActivityComponent
import dagger.hilt.android.components.ActivityRetainedComponent
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

@Module
@InstallIn(ActivityRetainedComponent::class)
class NetworkModule {

    @Provides
    fun provideWebservice(): Webservice {
        return Retrofit.Builder()
            .baseUrl("https://triforksocialkpi.azurewebsites.net")
            .addConverterFactory(GsonConverterFactory.create())
            .build()
            .create(Webservice::class.java)
    }

}
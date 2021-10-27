package com.trifork.triforkemployee.ui.employees

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.triforkemployee.databinding.FragmentEmployeesBinding
import com.trifork.triforkemployee.database.employee.Employee
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class EmployeesFragment : Fragment() {

    private val employeesViewModel : EmployeesViewModel by viewModels()
    private var _binding: FragmentEmployeesBinding? = null

    private val binding get() = _binding!!

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View {

        _binding = FragmentEmployeesBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val listView = binding.listViewEmployee
        val adapter = EmployeesListAdapter { employee -> adapterOnClick(employee) }
        listView.adapter = adapter
        listView.layoutManager = LinearLayoutManager(context)

        employeesViewModel.employees.observe(viewLifecycleOwner, {
            adapter.submitList(it)
            Log.d("Fragment", it.toString())
            adapter.notifyDataSetChanged()
        })

        return root
    }

    private fun adapterOnClick(employee: Employee) {
        Log.d("Fragment", employee.toString())
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}
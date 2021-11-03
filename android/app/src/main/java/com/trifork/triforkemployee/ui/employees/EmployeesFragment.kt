package com.trifork.triforkemployee.ui.employees

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.triforkemployee.databinding.FragmentEmployeesBinding
import com.trifork.triforkemployee.database.Employee
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
            adapter.notifyDataSetChanged()
        })

        return root
    }

    private fun adapterOnClick(employee: Employee) {
        val action = EmployeesFragmentDirections.actionNavEmployeesToNavEmployee(employee.id)
        findNavController().navigate(action)
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}
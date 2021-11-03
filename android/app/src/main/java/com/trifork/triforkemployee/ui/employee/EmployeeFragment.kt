package com.trifork.triforkemployee.ui.employee

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.navigation.fragment.navArgs
import com.example.triforkemployee.databinding.FragmentEmployeeBinding
import dagger.hilt.android.AndroidEntryPoint
import android.content.pm.ResolveInfo

import android.content.pm.PackageManager

import android.content.Intent
import android.net.Uri
import android.util.Log


@AndroidEntryPoint
class EmployeeFragment : Fragment() {

    private var _binding: FragmentEmployeeBinding? = null
    private val employeeViewModel: EmployeeViewModel by viewModels()
    private val binding get() = _binding!!

    private val args: EmployeeFragmentArgs by navArgs()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentEmployeeBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val employeeId = args.employeeId

        employeeViewModel.getEmployee(employeeId)

        employeeViewModel.employee.observe(viewLifecycleOwner) {
            binding.textViewEmployeeInitials.text = it.initials
            binding.textViewEmployeeName.text = it.firstName + " " + it.lastName
            binding.employeeCall.visibility = View.GONE
            binding.employeeMessage.visibility = View.GONE
        }

        binding.employeeCall.setOnClickListener {

        }

        binding.employeeMessage.setOnClickListener {

        }

        binding.employeeRocket.setOnClickListener {
            val initials = employeeViewModel.employee.value?.initials
            if(initials != null) {
                startRocketChat(initials)
            }
        }

        binding.employeeZoom.setOnClickListener {
            val initials = employeeViewModel.employee.value?.initials
            if(initials != null) {
                startZoom(initials)
            }
        }

        return root
    }

    private fun startIntent(uri: Uri) {
        startActivity(Intent(Intent.ACTION_VIEW, uri))
    }

    private fun startZoom(initials: String) {
        val zoomId = "zoomus://6073478739" //"https://trifork.zoom.us/c/6073478739"
        startIntent(Uri.parse(zoomId))
    }

    private fun startRocketChat(initials: String) {
        val rocketChat = "https://go.rocket.chat/room?host=chat.trifork.com&path=direct/$initials"
        val uri: Uri = Uri.parse(rocketChat)
        startIntent(uri)
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}
package com.trifork.triforkemployee.ui.events

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.viewModels
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.triforkemployee.databinding.FragmentEventsBinding
import com.trifork.triforkemployee.database.Event
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class EventsFragment : Fragment() {

    private val eventsViewModel: EventsViewModel by viewModels()
    private var _binding: FragmentEventsBinding? = null

    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentEventsBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val listView = binding.listViewEvents
        val adapter = EventsListAdapter(listOf()) { event -> adapterOnClick(event) }

        listView.adapter = adapter
        listView.layoutManager = LinearLayoutManager(context)

        eventsViewModel.events.observe(viewLifecycleOwner, {
            adapter.events = it
            adapter.notifyDataSetChanged()
        })


        return root
    }

    private fun adapterOnClick(event: Event) {
        //val action = EmployeesFragmentDirections.actionNavEmployeesToNavEmployee(event.id)
        //findNavController().navigate(action)
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}
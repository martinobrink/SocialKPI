package com.trifork.triforkemployee.ui.events

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.triforkemployee.databinding.ListItemEventBinding
import com.trifork.triforkemployee.database.Event

class EventsListAdapter(
    var events: List<Event>,
    private val onClick: (Event) -> Unit
) : RecyclerView.Adapter<EventsListAdapter.ViewHolder>() {

    class ViewHolder(binding: ListItemEventBinding, val onClick: (Event) -> Unit) : RecyclerView.ViewHolder(binding.root) {
        private val titleTextView = binding.textViewEventTitle
        private val categoryLettersTextView = binding.textViewCategoryLetters
        private val categoryTextView = binding.textViewCategory
        private val dateTimeTextView = binding.textViewTimeOfEvent

        private var currentEvent: Event? = null

        init {
            itemView.setOnClickListener {
                currentEvent?.let {
                    onClick(it)
                }
            }
        }

        fun bind(event: Event) {
            currentEvent = event

            titleTextView.text = currentEvent?.title
            categoryLettersTextView.text = currentEvent?.category
            categoryTextView.text = currentEvent?.category
            dateTimeTextView.text = currentEvent?.formatTimeOfEvent()
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = ListItemEventBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(view, onClick)
    }

    override fun onBindViewHolder(viewHolder: ViewHolder, position: Int) {
        viewHolder.bind(events[position])
    }

    override fun getItemCount(): Int {
        return events.size
    }
}
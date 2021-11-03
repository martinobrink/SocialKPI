package com.trifork.triforkemployee.ui.employees

import android.widget.TextView
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.example.triforkemployee.R
import com.trifork.triforkemployee.database.Employee

class EmployeesListAdapter(private val onClick: (Employee) -> Unit) : ListAdapter<Employee, EmployeesListAdapter.ViewHolder>(EmployeeDiffCallback) {

    class ViewHolder(view: View, val onClick: (Employee) -> Unit) : RecyclerView.ViewHolder(view) {
        private val initialsTextView: TextView = view.findViewById(R.id.text_view_category_letters)
        private val nameTextView: TextView = view.findViewById(R.id.text_view_event_title)
        private val departmentTextView: TextView = view.findViewById(R.id.text_view_category)
        private val emailTextView: TextView = view.findViewById(R.id.text_view_time_of_event)
        private var currentEmployee: Employee? = null

        init {
            itemView.setOnClickListener {
                currentEmployee?.let {
                    onClick(it)
                }
            }
        }

        /* Bind flower name and image. */
        fun bind(employee: Employee) {
            currentEmployee = employee
            nameTextView.text = employee.firstName + " " + employee.lastName
            initialsTextView.text = employee.initials
            departmentTextView.visibility = View.GONE
            emailTextView.text = employee.initials + "@trifork.com"
        }
    }

    // Create new views (invoked by the layout manager)
    override fun onCreateViewHolder(viewGroup: ViewGroup, viewType: Int): ViewHolder {
        // Create a new view, which defines the UI of the list item
        val view = LayoutInflater.from(viewGroup.context)
            .inflate(R.layout.list_item_employee, viewGroup, false)

        return ViewHolder(view, onClick)
    }

    override fun onBindViewHolder(viewHolder: ViewHolder, position: Int) {
        val employee = getItem(position)
        viewHolder.bind(employee)
    }
}

object EmployeeDiffCallback : DiffUtil.ItemCallback<Employee>() {
    override fun areItemsTheSame(oldItem: Employee, newItem: Employee): Boolean {
        return oldItem == newItem
    }

    override fun areContentsTheSame(oldItem: Employee, newItem: Employee): Boolean {
        return oldItem.initials == newItem.initials
    }
}
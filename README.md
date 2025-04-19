# 📡 Teacher Management - API Endpoints

This documentation outlines the ASP.NET Core MVC API methods related to **updating teacher data**. All database operations use MySQL.

---

## 🔧 `UpdateAPI(Teacher teacher)`

### ➤ Type: `PUT`  
### ➤ Route: `/UpdateTeacherAPI`

**Description:**  
Updates a teacher's details in the database using values passed from the controller.

**Parameters:**  
- `teacher` (Teacher model): Must include `TeacherId`, `TeacherFirstName`, `TeacherLastName`, `EmployeeID`, `HireDate`, and `Salary`.

**Logic:**  
Performs a `MySQL UPDATE` on the `teachers` table using parameterized queries to prevent SQL injection.

---

## 🔁 `UpdateTeacherAPIRoute(int ID, ...)`

### ➤ Type: `POST`  
### ➤ Route: `/TeacherPage/UpdateTeacherAPIRoute/{ID}`

**Description:**  
Acts as a controller route for accepting user input and calling the `UpdateAPI()` method to update teacher records.

**Parameters:**
- `ID` (int): Teacher ID passed in the URL.
- `TeacherFirstName` (string)
- `TeacherLastName` (string)
- `EmployeeID` (string)
- `HireDate` (DateTime)
- `Salary` (double)

**Returns:**
- `RedirectToAction("List")` – redirects to the teacher list view after a successful update.

---

## 🔍 `UpdateTeacher(int ID)`

### ➤ Type: `GET`  
### ➤ Route: `/TeacherPage/UpdateTeacher/{ID}`

**Description:**  
Fetches a teacher’s current data based on the ID and passes it to the view for updating.

**Parameters:**
- `ID` (int): The teacher’s unique identifier.

**Returns:**
- `View(teacher)` – the Razor view containing the pre-filled form for editing.

---

## 📦 Summary

| Method                     | Type | Route                                      | Description                         |
|---------------------------|------|--------------------------------------------|-------------------------------------|
| `UpdateAPI`               | PUT  | `/UpdateTeacherAPI`                        | Updates teacher in DB               |
| `UpdateTeacherAPIRoute`   | POST | `/TeacherPage/UpdateTeacherAPIRoute/{ID}`  | Takes form data & updates teacher   |
| `UpdateTeacher`           | GET  | `/TeacherPage/UpdateTeacher/{ID}`          | Loads teacher data for editing form |

---

✅ Created by **Yaqoob Zahoor Ahmed**  
📅 Last updated: April 18, 2025

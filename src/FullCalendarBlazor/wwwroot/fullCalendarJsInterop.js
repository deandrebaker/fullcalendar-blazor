import { FullCalendar } from "./fullcalendar/main.min.js";

export function render(data = {}) {
    var calendarElement = document.getElementById('fullcalendar-blazor');
    var calendar = new FullCalendar.Calendar(calendarElement, data);
    calendar.render();
}

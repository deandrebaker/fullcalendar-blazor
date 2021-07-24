import { FullCalendar } from "./fullcalendar/main.min.js";

export function render() {
    var calendarElement = document.getElementById('fullcalendar');
    var calendar = new FullCalendar.Calendar(calendarElement);
    calendar.render();
}
import {FullCalendar} from "./fullcalendar/main.min.js";

export function render(elementId, data) {
    var calendarElement = document.getElementById(elementId);
    var calendar = new FullCalendar.Calendar(calendarElement, data);
    calendar.render();
}

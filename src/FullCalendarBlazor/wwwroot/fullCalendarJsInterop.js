import {FullCalendar} from "./fullcalendar/main.min.js";

export function render(elementId, serializedData) {
    var calendarElement = document.getElementById(elementId);
    var calendar = new FullCalendar.Calendar(calendarElement, JSON.parse(serializedData));
    calendar.render();
}

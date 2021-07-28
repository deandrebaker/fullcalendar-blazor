import {FullCalendar} from "./fullcalendar/main.min.js";

export function render(elementId, serializedData, objRef) {
    var calendarData = JSON.parse(serializedData);
    
    calendarData.eventClick = function(info) {
        objRef.invokeMethodAsync('EventClick', info);
    };

    calendarData.eventMouseEnter = function(info) {
        objRef.invokeMethodAsync('EventMouseEnter', info);
    };

    calendarData.eventMouseLeave = function(info) {
        objRef.invokeMethodAsync('EventMouseLeave', info);
    };
    
    var calendarElement = document.getElementById(elementId);
    var calendar = new FullCalendar.Calendar(calendarElement, calendarData);
    calendar.render();
}

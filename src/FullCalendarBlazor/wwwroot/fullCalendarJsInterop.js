import {FullCalendar} from "./fullcalendar/main.min.js";

// Globals

const calendars = {};

export function render(serializedData, serializedMethodData, objRef) {
    const { id, ...calendarData} = JSON.parse(serializedData);
    const calendarMethodData = JSON.parse(serializedMethodData);

    // Transform Calendar Properties

    ["headerToolbar", "footerToolbar", "listDayFormat", "listDaySideFormat"]
        .forEach(propName => {
            const omitPropName = `omit${propName[0].toUpperCase()}${propName.substr(1)}`;
            if (calendarData[omitPropName]) calendarData[propName] = false;
            delete calendarData[omitPropName];
        });

    if (calendarData.limitDayEventRowsToHeight) {
        calendarData.dayMaxEventRows = true;
        delete calendarData.limitDayEventRowsToHeight;
    }

    if (calendarData.limitDayEventsToHeight) {
        calendarData.dayMaxEvents = true;
        delete calendarData.limitDayEventsToHeight;
    }

    ["headerToolbar", "footerToolbar"]
        .forEach(propName => 
            Object.getOwnPropertyNames(calendarData[propName] ?? {})
                .forEach(innerPropName => 
                    calendarData[propName][innerPropName] = calendarData[propName][innerPropName].join(''))
        );

    ["titleFormat", "listDayFormat", "listDaySideFormat", "dayHeaderFormat", "slotLabelFormat", "weekNumberFormat", "eventTimeFormat", "dayPopoverFormat"]
        .forEach(propName => {
            if (calendarData[propName]?.omitMeridiem) calendarData[propName].meridiem = false;
            delete calendarData[propName]?.omitMeridiem;
        });

    Object.getOwnPropertyNames(calendarMethodData).forEach(propName => 
        calendarData[propName] = (...args) => objRef.invokeMethod(calendarMethodData[propName], ...args) 
    )

    const calendarElement = document.getElementById(id);
    calendars[id] = new FullCalendar.Calendar(calendarElement, calendarData);
    calendars[id].render();
}

export function executeMethod(elementId, methodName, args) {
    return calendars[elementId][methodName](...args);
}

export function getProperty(elementId, propName) {
    return calendars[elementId][propName];
}

export function print(obj) {
    console.log(obj);
}
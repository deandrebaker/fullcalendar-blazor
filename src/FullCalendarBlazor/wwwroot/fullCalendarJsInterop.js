import {FullCalendar} from "./fullcalendar/main.min.js";

// Globals

const calendars = {};

export function render(elementId, serializedData, objRef) {
    const calendarData = JSON.parse(serializedData);

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

    // Calendar functions

    // region Overall Display

    calendarData.windowResize = (arg) => objRef.invokeMethod('WindowResize', arg.view);

    // endregion

    // region Views

    calendarData.allDayDidMount = (arg) => objRef.invokeMethod('AllDayDidMount', arg.text);
    calendarData.allDayWillUnmount = (arg) => objRef.invokeMethod('AllDayWillUnmount', arg.text);
    calendarData.noEventsDidMount = (arg) => objRef.invokeMethod('NoEventsDidMount', arg.el);
    calendarData.noEventsWillUnmount = (arg) => objRef.invokeMethod('NoEventsWillUnmount', arg.el);
    if (calendarData.useGetVisibleRange) calendarData.visibleRange = (currentDate) => objRef.invokeMethod('GetVisibleRange', currentDate);
    calendarData.viewDidMount = (arg) => objRef.invokeMethod('ViewDidMount', arg.view, arg.el);
    calendarData.viewWillUnmount = (arg) => objRef.invokeMethod('ViewWillUnmount', arg.view, arg.el);

    // endregion

    // region Date and Time

    calendarData.dayHeaderDidMount = (dayHeaderRenderInfo) => objRef.invokeMethod('DayHeaderDidMount', dayHeaderRenderInfo);
    calendarData.dayHeaderWillUnmount = (dayHeaderRenderInfo) => objRef.invokeMethod('DayHeaderWillUnmount', dayHeaderRenderInfo);
    calendarData.dayCellDidMount = (dayCellRenderInfo) => objRef.invokeMethod('DayCellDidMount', dayCellRenderInfo);
    calendarData.dayCellWillUnmount = (dayCellRenderInfo) => objRef.invokeMethod('DayCellWillUnmount', dayCellRenderInfo);
    calendarData.slotLabelDidMount = (slotRenderInfo) => objRef.invokeMethod('SlotLabelDidMount', slotRenderInfo);
    calendarData.slotLabelWillUnmount = (slotRenderInfo) => objRef.invokeMethod('SlotLabelWillUnmount', slotRenderInfo);
    calendarData.slotLaneDidMount = (slotRenderInfo) => objRef.invokeMethod('SlotLaneDidMount', slotRenderInfo);
    calendarData.slotLaneWillUnmount = (slotRenderInfo) => objRef.invokeMethod('SlotLaneWillUnmount', slotRenderInfo);
    calendarData.datesSet = (dateInfo) => objRef.invokeMethod('DatesSet', dateInfo);
    if (calendarData.useNavLinkDayClickMethod) calendarData.navLinkDayClick = (date, jsEvent) => objRef.invokeMethod('NavLinkDayClickMethod', date, jsEvent);
    if (calendarData.useNavLinkWeekClickMethod) calendarData.navLinkWeekClick = (weekStart, jsEvent) => objRef.invokeMethod('NavLinkWeekClickMethod', weekStart, jsEvent);
    if (calendarData.useGetWeekNumber) calendarData.weekNumberCalculation = (date) => objRef.invokeMethod('GetWeekNumber', date);
    calendarData.weekNumberDidMount = (num, text, date) => objRef.invokeMethod('WeekNumberDidMount', num, text, date);
    calendarData.weekNumberWillUnmount = (num, text, date) => objRef.invokeMethod('WeekNumberWillUnmount', num, text, date);
    if (calendarData.useGetSelectOverlap) calendarData.selectOverlap = (overlappedEvent) => objRef.invokeMethod('GetSelectOverlap', overlappedEvent);
    if (calendarData.useGetSelectAllow) calendarData.selectAllow = (selectInfo) => objRef.invokeMethod('GetSelectAllow', selectInfo);
    calendarData.nowIndicatorDidMount = (nowIndicatorInfo) => objRef.invokeMethod('NowIndicatorDidMount', nowIndicatorInfo);
    calendarData.dateClick = (dateClickInfo) => objRef.invokeMethod('DateClick', dateClickInfo);
    calendarData.select = (selectionInfo) => objRef.invokeMethod('Select', selectionInfo);
    calendarData.unselect = (jsEvent, view) => objRef.invokeMethod('Unselect', jsEvent, view);
    if (calendarData.useGetNow) calendarData.now = () => objRef.invokeMethod('GetNow');
    calendarData.nowIndicatorDidMount = (nowIndicatorInfo) => objRef.invokeMethod('NowIndicatorDidMount', nowIndicatorInfo);
    calendarData.nowIndicatorWillUnmount = (nowIndicatorInfo) => objRef.invokeMethod('NowIndicatorWillUnmount', nowIndicatorInfo);

    // endregion

    // region Events

    if (calendarData.useEventDataTransform) calendarData.eventDataTransform = (eventData) => objRef.invokeMethod('EventDataTransform', eventData) ?? false;
    calendarData.eventAdd = (eventAddInfo) => objRef.invokeMethod('EventAdd', eventAddInfo);
    calendarData.eventChange = (eventChangeInfo) => objRef.invokeMethod('EventChange', eventChangeInfo);
    calendarData.eventRemove = (eventRemoveInfo) => objRef.invokeMethod('EventRemove', eventRemoveInfo);
    calendarData.eventsSet = (events) => objRef.invokeMethod('EventsSet', events);
    if (calendarData.useGetEventOrder) calendarData.eventOrder = (eventA, eventB) => objRef.invokeMethod('GetEventOrder', eventA, eventB);
    calendarData.eventDidMount = (eventRenderInfo) => objRef.invokeMethod('EventDidMount', eventRenderInfo);
    calendarData.eventWillUnmount = (eventRenderInfo) => objRef.invokeMethod('EventWillUnmount', eventRenderInfo);
    calendarData.eventClick = (eventClickInfo) => objRef.invokeMethod('EventClick', eventClickInfo);
    calendarData.eventMouseEnter = (mouseEnterInfo) => objRef.invokeMethod('EventMouseEnter', mouseEnterInfo);
    calendarData.eventMouseLeave = (mouseLeaveInfo) => objRef.invokeMethod('EventMouseLeave', mouseLeaveInfo);
    if (calendarData.useEventOverlapMethod) calendarData.eventOverlap = (stillEvent, movingEvent) => objRef.invokeMethod('EventOverlapMethod', stillEvent, movingEvent);
    calendarData.eventAllow = (eventAllowInfo, draggedEvent) => objRef.invokeMethod('EventAllow', eventAllowInfo, draggedEvent);
    if (calendarData.useDropAcceptMethod) calendarData.dropAccept = (draggableEl) => objRef.invokeMethod('DropAcceptMethod', draggableEl);
    calendarData.eventDragStart = (eventDragInfo) => objRef.invokeMethod('EventDragStart', eventDragInfo);
    calendarData.eventDragStop = (eventDragInfo) => objRef.invokeMethod('EventDragStop', eventDragInfo);
    calendarData.eventDrop = (eventDropInfo) => objRef.invokeMethod('EventDrop', eventDropInfo);
    calendarData.drop = (dropInfo) => objRef.invokeMethod('Drop', dropInfo);
    calendarData.eventReceive = (eventReceiveInfo) => objRef.invokeMethod('EventReceive', eventReceiveInfo);
    calendarData.eventLeave = (eventLeaveInfo) => objRef.invokeMethod('EventLeave', eventLeaveInfo);
    calendarData.eventResizeStart = (eventResizeInfo) => objRef.invokeMethod('EventResizeStart', eventResizeInfo);
    calendarData.eventResizeStop = (eventResizeInfo) => objRef.invokeMethod('EventResizeStop', eventResizeInfo);
    calendarData.eventResize = (eventResizeInfo) => objRef.invokeMethod('EventResize', eventResizeInfo);
    if (calendarData.useMoreLinkClickMethod) calendarData.moreLinkClick = (moreLinkClickInfo) => objRef.invokeMethod('MoreLinkClickMethod', moreLinkClickInfo);
    calendarData.moreLinkDidMount = (num, text) => objRef.invokeMethod('MoreLinkDidMount', num, text);
    calendarData.moreLinkWillUnmount = (num, text) => objRef.invokeMethod('MoreLinkWillUnmount', num, text);

    // endregion

    const calendarElement = document.getElementById(elementId);
    calendars[elementId] = new FullCalendar.Calendar(calendarElement, calendarData);
    calendars[elementId].render();
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
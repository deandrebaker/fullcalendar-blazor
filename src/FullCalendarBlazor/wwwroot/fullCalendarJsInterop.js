import {FullCalendar} from "./fullcalendar/main.min.js";

export function render(elementId, serializedData, objRef) {
    var calendarData = JSON.parse(serializedData);

    // Calendar functions
    calendarData.windowResize = (arg) => objRef.invokeMethod('WindowResize', arg.view);
    // calendarData.allDayClassNames = (arg) => objRef.invokeMethod('AllDayClassNames', arg); // Todo
    // calendarData.allDayContent = (arg) => objRef.invokeMethod('AllDayContent', arg); // Todo
    calendarData.allDayDidMount = (arg) => objRef.invokeMethod('AllDayDidMount', arg.text);
    calendarData.allDayWillUnmount = (arg) => objRef.invokeMethod('AllDayWillUnmount', arg.text);
    // calendarData.noEventsClassNames = (arg) => objRef.invokeMethod('NoEventsClassNames', arg); // Todo
    // calendarData.noEventsContent = (arg) => objRef.invokeMethod('NoEventsContent', arg); // Todo
    calendarData.noEventsDidMount = (arg) => objRef.invokeMethod('NoEventsDidMount', arg.el);
    calendarData.noEventsWillUnmount = (arg) => objRef.invokeMethod('NoEventsWillUnmount', arg.el);
    // calendarData.viewClassNames = (arg) => objRef.invokeMethod('ViewClassNames', arg); // Todo
    calendarData.viewDidMount = (arg) => objRef.invokeMethod('ViewDidMount', arg.view, arg.el);
    calendarData.viewWillUnmount = (arg) => objRef.invokeMethod('ViewWillUnmount', arg.view, arg.el);
    calendarData.eventAdd = (eventAddInfo) => objRef.invokeMethod('EventAdd', eventAddInfo);
    calendarData.eventChange = (eventChangeInfo) => objRef.invokeMethod('EventChange', eventChangeInfo);
    calendarData.eventRemove = (eventRemoveInfo) => objRef.invokeMethod('EventRemove', eventRemoveInfo);
    calendarData.eventsSet = (events) => objRef.invokeMethod('EventsSet', events);
    // calendarData.eventClassNames = (eventRenderInfo) => objRef.invokeMethod('EventClassNames', eventRenderInfo); // Todo
    // calendarData.eventContent = (eventRenderInfo) => objRef.invokeMethod('EventContent', eventRenderInfo); // Todo
    calendarData.eventDidMount = (eventRenderInfo) => objRef.invokeMethod('EventDidMount', eventRenderInfo);
    calendarData.eventWillUnmount = (eventRenderInfo) => objRef.invokeMethod('EventWillUnmount', eventRenderInfo);
    calendarData.eventClick = (eventClickInfo) => objRef.invokeMethod('EventClick', eventClickInfo);
    calendarData.eventMouseEnter = (mouseEnterInfo) => objRef.invokeMethod('EventMouseEnter', mouseEnterInfo);
    calendarData.eventMouseLeave = (mouseLeaveInfo) => objRef.invokeMethod('EventMouseLeave', mouseLeaveInfo);
    calendarData.eventOverlap = (stillEvent, movingEvent) => objRef.invokeMethod('EventOverlap', stillEvent, movingEvent);
    calendarData.eventAllow = (eventAllowInfo, draggedEvent) => objRef.invokeMethod('EventAllow', eventAllowInfo, draggedEvent);
    calendarData.dropAccept = (draggableEl) => objRef.invokeMethod('DropAccept', draggableEl);
    calendarData.eventDragStart = (eventDragInfo) => objRef.invokeMethod('EventDragStart', eventDragInfo);
    calendarData.eventDragStop = (eventDragInfo) => objRef.invokeMethod('EventDragStop', eventDragInfo);
    calendarData.eventDrop = (eventDropInfo) => objRef.invokeMethod('EventDrop', eventDropInfo);
    calendarData.drop = (dropInfo) => objRef.invokeMethod('Drop', dropInfo);
    calendarData.eventReceive = (eventReceiveInfo) => objRef.invokeMethod('EventReceive', eventReceiveInfo);
    calendarData.eventLeave = (eventLeaveInfo) => objRef.invokeMethod('EventLeave', eventLeaveInfo);
    calendarData.eventResizeStart = (eventResizeInfo) => objRef.invokeMethod('EventResizeStart', eventResizeInfo);
    calendarData.eventResizeStop = (eventResizeInfo) => objRef.invokeMethod('EventResizeStop', eventResizeInfo);
    calendarData.eventResize = (eventResizeInfo) => objRef.invokeMethod('EventResize', eventResizeInfo);
    // calendarData.moreLinkClassNames = (arg) => objRef.invokeMethod('MoreLinkClassNames', arg.num, arg.text); // Todo
    // calendarData.moreLinkContent = (arg) => objRef.invokeMethod('MoreLinkContent', arg.num, arg.text); // Todo
    calendarData.moreLinkDidMount = (num, text) => objRef.invokeMethod('MoreLinkDidMount', num, text);
    calendarData.moreLinkWillUnmount = (num, text) => objRef.invokeMethod('MoreLinkWillUnmount', num, text);

    var calendarElement = document.getElementById(elementId);
    var calendar = new FullCalendar.Calendar(calendarElement, calendarData);
    calendar.render();
}

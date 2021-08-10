using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Display;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.International;
using FullCalendarBlazor.Models.Views;
using FullCalendarBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        // Injected Dependencies
        [Inject] private IJSRuntimeService JsInterop { get; set; }

        // Parameters

        [Parameter] public string Id { get; set; }

        #region Overall Display

        [Parameter] public Toolbar HeaderToolbar { get; set; }
        [Parameter] public bool? OmitHeaderToolbar { get; set; }
        [Parameter] public Toolbar FooterToolbar { get; set; }
        [Parameter] public bool? OmitFooterToolbar { get; set; }
        [Parameter] public DateTimeFormatter TitleFormat { get; set; }
        [Parameter] public string TitleRangeSeparator { get; set; }
        [Parameter] public Dictionary<string, string> ButtonText { get; set; }
        [Parameter] public Dictionary<string, string> ButtonIcons { get; set; }
        [Parameter] public Dictionary<string, CustomButton> CustomButtons { get; set; }
        [Parameter] public ThemeSystemOption? ThemeSystem { get; set; }
        // Todo: Add configuration for Bootstrap theming
        [Parameter] public string Height { get; set; }
        [Parameter] public string ContentHeight { get; set; }
        [Parameter] public double? AspectRatio { get; set; }
        [Parameter] public bool? ExpandRows { get; set; }
        // Todo: Add method for UpdateSize
        [Parameter] public bool? HandleWindowResize { get; set; }
        [Parameter] public int? WindowResizeDelay { get; set; }
        [Parameter] public bool? StickyHeaderDates { get; set; }
        [Parameter] public bool? StickyFooterScrollbar { get; set; }
        [Parameter] public Action<View> OnWindowResize { get; set; }

        #endregion

        #region Views

        [Parameter] public bool? FixedWeekCount { get; set; }
        [Parameter] public bool? ShowNonCurrentDates { get; set; }
        [Parameter] public int? EventMinHeight { get; set; }
        [Parameter] public int? EventShortHeight { get; set; }
        [Parameter] public bool? SlotEventOverlap { get; set; }
        [Parameter] public bool? AllDaySlot { get; set; }
        // Todo: Add OnAllDayClassNames EventCallback
        // Todo: Add OnAllDayContent EventCallback
        [Parameter] public Action<string> OnAllDayDidMount { get; set; }
        [Parameter] public Action<string> OnAllDayWillUnmount { get; set; }
        [Parameter] public DateTimeFormatter ListDayFormat { get; set; }
        [Parameter] public bool? OmitListDayFormat { get; set; }
        [Parameter] public DateTimeFormatter ListDaySideFormat { get; set; }
        [Parameter] public bool? OmitListDaySideFormat { get; set; }
        // Todo: Add OnNoEventsClassNames EventCallback
        // Todo: Add OnNoEventsContent EventCallback
        [Parameter] public Action<object> OnNoEventsDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<object> OnNoEventsWillUnmount { get; set; } // Todo: Replace object with proper type
        [Parameter] public TimeSpan? Duration { get; set; }
        [Parameter] public int? DayCount { get; set; }
        [Parameter] public DateRange VisibleRange { get; set; }
        [Parameter] public Func<DateTime, DateRange> OnGetVisibleRange { get; set; }
        // Todo: Add support for custom views
        [Parameter] public ViewOption? InitialView { get; set; }
        [Parameter] public Dictionary<string, object> Views { get; set; }
        // Todo: Add method to get current view
        // Todo: Add method to change view
        // Todo: Add OnViewClassNames EventCallback
        [Parameter] public Action<View, object> OnViewDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<View, object> OnViewWillUnmount { get; set; } // Todo: Replace object with proper type

        #endregion

        #region Date and Time

        [Parameter] public bool? Weekends { get; set; }
        [Parameter] public IEnumerable<DayOfWeek> HiddenDays { get; set; }
        [Parameter] public bool? DayHeaders { get; set; }
        [Parameter] public DateTimeFormatter DayHeaderFormat { get; set; }
        [Parameter] public int? DayMinWidth { get; set; }
        // Todo: OnDayHeaderClassNames
        // Todo: OnDayHeaderContent
        [Parameter] public Action<DayHeaderRenderInfo> OnDayHeaderDidMount { get; set; }
        [Parameter] public Action<DayHeaderRenderInfo> OnDayHeaderWillUnmount { get; set; }
        // Todo: OnDayCellClassNames
        // Todo: OnDayCellContent
        [Parameter] public Action<DayCellRenderInfo> OnDayCellDidMount { get; set; }
        [Parameter] public Action<DayCellRenderInfo> OnDayCellWillUnmount { get; set; }
        [Parameter] public TimeSpan? SlotDuration { get; set; }
        [Parameter] public TimeSpan? SlotLabelInterval { get; set; }
        [Parameter] public DateTimeFormatter SlotLabelFormat { get; set; }
        [Parameter] public TimeSpan? SlotMinTime { get; set; }
        [Parameter] public TimeSpan? SlotMaxTime { get; set; }
        [Parameter] public TimeSpan? ScrollTime { get; set; }
        [Parameter] public bool? ScrollTimeReset { get; set; }
        // Todo: OnSlotLabelClassNames
        // Todo: OnSlotLabelContent
        [Parameter] public Action<SlotRenderInfo> OnSlotLabelDidMount { get; set; }
        [Parameter] public Action<SlotRenderInfo> OnSlotLabelWillUnmount { get; set; }
        // Todo: OnSlotLaneClassNames
        // Todo: OnSlotLaneContent
        [Parameter] public Action<SlotRenderInfo> OnSlotLaneDidMount { get; set; }
        [Parameter] public Action<SlotRenderInfo> OnSlotLaneWillUnmount { get; set; }
        // Todo: Add method for scrollToTime
        [Parameter] public Action<DateInfo> OnDatesSet { get; set; }
        [Parameter] public DateTime? InitialDate { get; set; }
        [Parameter] public TimeSpan? DateIncrement { get; set; }
        [Parameter] public DateAlignmentOption? DateAlignment { get; set; }
        [Parameter] public DateRange ValidRange { get; set; }
        // Todo: Calendar::prev() method
        // Todo: Calendar::next() method
        // Todo: Calendar::prevYear() method
        // Todo: Calendar::nextYear() method
        // Todo: Calendar::today() method
        // Todo: Calendar::gotoDate() method
        // Todo: Calendar::getDate() method
        [Parameter] public bool? NavLinks { get; set; }
        [Parameter] public string NavLinkDayClick { get; set; }
        [Parameter] public Action<DateTime, object> OnNavLinkDayClick { get; set; }
        [Parameter] public string NavLinkWeekClick { get; set; }
        [Parameter] public Action<DateTime, object> OnNavLinkWeekClick { get; set; }
        [Parameter] public bool? WeekNumbers { get; set; }
        [Parameter] public WeekNumberOption? WeekNumberCalculation { get; set; }
        [Parameter] public Func<DateTime, int> OnGetWeekNumber { get; set; }
        [Parameter] public string WeekText { get; set; }
        [Parameter] public DateTimeFormatter WeekNumberFormat { get; set; }
        // Todo: OnWeekNumberClassNames
        // Todo: OnWeekNumberContent
        [Parameter] public Action<string, string, DateTime> OnWeekNumberDidMount { get; set; }
        [Parameter] public Action<string, string, DateTime> OnWeekNumberWillUnmount { get; set; }
        [Parameter] public bool? Selectable { get; set; }
        [Parameter] public bool? SelectMirror { get; set; }
        [Parameter] public bool? UnselectAuto { get; set; }
        [Parameter] public string UnselectCancel { get; set; }
        [Parameter] public bool? SelectOverlap { get; set; }
        [Parameter] public Func<Event, bool> OnGetSelectOverlap { get; set; }
        [Parameter] public object SelectConstraint { get; set; }
        [Parameter] public Func<SelectInfo, bool> OnGetSelectAllow { get; set; }
        [Parameter] public int? SelectMinDistance { get; set; }
        [Parameter] public Action<DateClickInfo> OnDateClick { get; set; }
        [Parameter] public Action<SelectionInfo> OnSelect { get; set; }
        [Parameter] public Action<object, View> OnUnselect { get; set; } // Todo
        // Todo: Calendar::select()
        // Todo: Calendar::unselect()
        [Parameter] public bool? NowIndicator { get; set; }
        [Parameter] public DateTime? Now { get; set; }
        [Parameter] public Func<DateTime> OnGetNow { get; set; }
        // Todo: OnNowIndicatorClassNames
        // Todo: OnNowIndicatorContent
        [Parameter] public Action<NowIndicatorInfo> OnNowIndicatorDidMount { get; set; }
        [Parameter] public Action<NowIndicatorInfo> OnNowIndicatorWillUnmount { get; set; }
        [Parameter] public object BusinessHours { get; set; }

        #endregion

        #region Events

        [Parameter] public IEnumerable<Event> Events { get; set; }
        [Parameter] public Func<object, Event> OnEventDataTransform { get; set; }
        [Parameter] public bool? DefaultAllDay { get; set; }
        [Parameter] public TimeSpan? DefaultAllDayEventDuration { get; set; }
        [Parameter] public TimeSpan? DefaultTimedEventDuration { get; set; }
        [Parameter] public bool? ForceEventDuration { get; set; }
        // Todo: Calendar::getEvents() method
        // Todo: Calendar::getEventsById() method
        // Todo: Calendar::addEvent() method
        [Parameter] public Action<EventAddInfo> OnEventAdd { get; set; }
        [Parameter] public Action<EventChangeInfo> OnEventChange { get; set; }
        [Parameter] public Action<EventAddInfo> OnEventRemove { get; set; }
        [Parameter] public Action<IEnumerable<Event>> OnEventsSet { get; set; }
        // Todo: Event sources
        [Parameter] public string EventColor { get; set; }
        [Parameter] public string EventBackgroundColor { get; set; }
        [Parameter] public string EventBorderColor { get; set; }
        [Parameter] public string EventTextColor { get; set; }
        [Parameter] public EventDisplayOption? EventDisplay { get; set; }
        [Parameter] public DateTimeFormatter EventTimeFormat { get; set; }
        [Parameter] public bool? DisplayEventTime { get; set; }
        [Parameter] public bool? DisplayEventEnd { get; set; }
        [Parameter] public TimeSpan? NextDayThreshold { get; set; }
        [Parameter] public IEnumerable<string> EventOrder { get; set; }
        [Parameter] public Func<Event, Event, int> OnGetEventOrder { get; set; }
        [Parameter] public bool? EventOrderStrict { get; set; }
        [Parameter] public bool? ProgressiveEventRendering { get; set; }
        // [Parameter] public Func<EventRenderInfo, IEnumerable<string>> OnEventClassNames { get; set; } // Todo
        // [Parameter] public Func<EventRenderInfo, object> OnEventContent { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<EventRenderInfo> OnEventDidMount { get; set; }
        [Parameter] public Action<EventRenderInfo> OnEventWillUnmount { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventClick { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseEnter { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseLeave { get; set; }
        [Parameter] public bool? Editable { get; set; }
        [Parameter] public bool? EventStartEditable { get; set; }
        [Parameter] public bool? EventResizableFromStart { get; set; }
        [Parameter] public bool? EventDurationEditable { get; set; }
        [Parameter] public bool? EventResourceEditable { get; set; }
        [Parameter] public bool? Droppable { get; set; }
        [Parameter] public int? EventDragMinDistance { get; set; }
        [Parameter] public int? DragRevertDuration { get; set; }
        [Parameter] public bool? DragScroll { get; set; }
        [Parameter] public TimeSpan? SnapDuration { get; set; }
        [Parameter] public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent parameter (https://fullcalendar.io/docs/fixedMirrorParent)
        [Parameter] public bool? EventOverlap { get; set; }
        [Parameter] public Func<Event, Event, bool> OnEventOverlap { get; set; }
        [Parameter] public object EventConstraint { get; set; }
        [Parameter] public Func<EventAllowInfo, Event, bool> OnEventAllow { get; set; }
        [Parameter] public string DropAccept { get; set; }
        [Parameter] public Func<object, bool> OnDropAccept { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventDragStart { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventDragStop { get; set; }
        [Parameter] public Action<EventDropInfo> OnEventDrop { get; set; }
        [Parameter] public Action<DropInfo> OnDrop { get; set; }
        [Parameter] public Action<ExternalEventDropInfo> OnEventReceive { get; set; }
        [Parameter] public Action<ExternalEventDropInfo> OnEventLeave { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventResizeStart { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventResizeStop { get; set; }
        [Parameter] public Action<EventResizeInfo> OnEventResize { get; set; }
        [Parameter] public int? DayMaxEventRows { get; set; }
        [Parameter] public bool? LimitDayEventRowsToHeight { get; set; }
        [Parameter] public int? DayMaxEvents { get; set; }
        [Parameter] public bool? LimitDayEventsToHeight { get; set; }
        [Parameter] public int? EventMaxStack { get; set; }
        [Parameter] public string MoreLinkClick { get; set; }
        [Parameter] public Action<MoreLinkClickInfo> OnMoreLinkClick { get; set; }
        [Parameter] public DateTimeFormatter DayPopoverFormat { get; set; }
        // [Parameter] public Func<int, string, IEnumerable<string>> OnMoreLinkClassNames { get; set; } // Todo
        // [Parameter] public Func<int, string, object> OnMoreLinkContent { get; set; } // Todo: Replace object with proper type // Todo
        [Parameter] public Action<int, string> OnMoreLinkDidMount { get; set; }
        [Parameter] public Action<int, string> OnMoreLinkWillUnmount { get; set; }

        #endregion

        #region International

        [Parameter] public string Locale { get; set; }
        [Parameter] public DirectionOption? Direction { get; set; }
        [Parameter] public DayOfWeek? FirstDay { get; set; }
        [Parameter] public string TimeZone { get; set; }

        #endregion

        // JSInvokable methods

        #region Overall Display

        [JSInvokable] public void WindowResize(View view) => OnWindowResize?.Invoke(view);

        #endregion

        #region Views

        // [JSInvokable] public IEnumerable<string> AllDayClassNames(object arg) => OnAllDayClassNames?.Invoke(arg) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object AllDayContent(object arg) => OnAllDayContent?.Invoke(arg); // Todo
        [JSInvokable] public void AllDayDidMount(string text) => OnAllDayDidMount?.Invoke(text);
        [JSInvokable] public void AllDayWillUnmount(string text) => OnAllDayWillUnmount?.Invoke(text);
        // [JSInvokable] public IEnumerable<string> NoEventsClassNames(object arg) => OnNoEventsClassNames?.Invoke(arg) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object NoEventsContent(object arg) => OnNoEventsContent?.Invoke(arg); // Todo
        [JSInvokable] public void NoEventsDidMount(object el) => OnNoEventsDidMount?.Invoke(el);
        [JSInvokable] public void NoEventsWillUnmount(object el) => OnNoEventsWillUnmount?.Invoke(el);
        [JSInvokable] public DateRange GetVisibleRange(DateTime dateTime) => OnGetVisibleRange?.Invoke(dateTime);
        // [JSInvokable] public IEnumerable<string> ViewClassNames(object arg) => OnViewClassNames?.Invoke(arg) ?? Enumerable.Empty<string>(); // Todo
        [JSInvokable] public void ViewDidMount(View view, object el) => OnViewDidMount?.Invoke(view, el);
        [JSInvokable] public void ViewWillUnmount(View view, object el) => OnViewWillUnmount?.Invoke(view, el);

        #endregion Views

        #region Date and Time

        [JSInvokable] public void DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderDidMount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderWillUnmount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void DayCellDidMount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellDidMount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellWillUnmount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void SlotLabelDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLabelDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLabelWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLaneDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLaneDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLaneWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void DatesSet(DateInfo dateInfo) => OnDatesSet?.Invoke(dateInfo);
        [JSInvokable] public void NavLinkDayClickMethod(DateTime date, object jsEvent) => OnNavLinkDayClick?.Invoke(date, jsEvent);
        [JSInvokable] public void NavLinkWeekClickMethod(DateTime weekStart, object jsEvent) => OnNavLinkWeekClick?.Invoke(weekStart, jsEvent);
        [JSInvokable] public int? GetWeekNumber(DateTime date) => OnGetWeekNumber?.Invoke(date);
        [JSInvokable] public void WeekNumberDidMount(string num, string text, DateTime date) => OnWeekNumberDidMount?.Invoke(num, text, date);
        [JSInvokable] public void WeekNumberWillUnmount(string num, string text, DateTime date) => OnWeekNumberWillUnmount?.Invoke(num, text, date);
        [JSInvokable] public bool? GetSelectOverlap(Event overlappedEvent) => OnGetSelectOverlap?.Invoke(overlappedEvent);
        [JSInvokable] public bool? GetSelectAllow(SelectInfo selectInfo) => OnGetSelectAllow?.Invoke(selectInfo);
        [JSInvokable] public void DateClick(DateClickInfo dateClickInfo) => OnDateClick?.Invoke(dateClickInfo);
        [JSInvokable] public void Select(SelectionInfo selectionInfo) => OnSelect?.Invoke(selectionInfo);
        [JSInvokable] public void Unselect(object jsEvent, View view) => OnUnselect?.Invoke(jsEvent, view);
        [JSInvokable] public DateTime? GetNow() => OnGetNow?.Invoke();
        [JSInvokable] public void NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorDidMount?.Invoke(nowIndicatorInfo);
        [JSInvokable] public void NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorWillUnmount?.Invoke(nowIndicatorInfo);

        #endregion

        #region Event

        [JSInvokable] public Event EventDataTransform(object eventData) => OnEventDataTransform?.Invoke(eventData);
        [JSInvokable] public void EventAdd(EventAddInfo eventAddInfo) => OnEventAdd?.Invoke(eventAddInfo);
        [JSInvokable] public void EventRemove(EventChangeInfo eventChangeInfo) => OnEventChange?.Invoke(eventChangeInfo);
        [JSInvokable] public void EventChange(EventAddInfo eventRemoveInfo) => OnEventRemove?.Invoke(eventRemoveInfo);
        [JSInvokable] public void EventsSet(IEnumerable<Event> events) => OnEventsSet?.Invoke(events);
        [JSInvokable] public int? GetEventOrder(Event eventA, Event eventB) => OnGetEventOrder?.Invoke(eventA, eventB);
        // [JSInvokable] public IEnumerable<string> EventClassNames(EventRenderInfo eventRenderInfo) => OnEventClassNames?.Invoke(eventRenderInfo) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object EventContent(EventRenderInfo eventRenderInfo) => OnEventContent?.Invoke(eventRenderInfo); // Todo
        [JSInvokable] public void EventDidMount(EventRenderInfo eventRenderInfo) => OnEventDidMount?.Invoke(eventRenderInfo);
        [JSInvokable] public void EventWillUnmount(EventRenderInfo eventRenderInfo) => OnEventWillUnmount?.Invoke(eventRenderInfo);
        [JSInvokable] public void EventClick(EventClickInfo eventClickInfo) => OnEventClick?.Invoke(eventClickInfo);
        [JSInvokable] public void EventMouseEnter(EventClickInfo mouseEnterInfo) => OnEventMouseEnter?.Invoke(mouseEnterInfo);
        [JSInvokable] public void EventMouseLeave(EventClickInfo mouseLeaveInfo) => OnEventMouseLeave?.Invoke(mouseLeaveInfo);
        [JSInvokable] public bool EventOverlapMethod(Event stillEvent, Event movingEvent) => OnEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool EventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent) => OnEventAllow?.Invoke(eventAllowInfo, draggedEvent) ?? true;
        [JSInvokable] public bool DropAcceptMethod(object draggableItem) => OnDropAccept?.Invoke(draggableItem) ?? true; // Todo: Replace object with DraggableItem type.
        [JSInvokable] public void EventDragStart(EventDragInfo eventDragInfo) => OnEventDragStart?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDragStop(EventDragInfo eventDragInfo) => OnEventDragStop?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDrop(EventDropInfo eventDropInfo) => OnEventDrop?.Invoke(eventDropInfo);
        [JSInvokable] public void Drop(DropInfo dropInfo) => OnDrop?.Invoke(dropInfo);
        [JSInvokable] public void EventReceive(ExternalEventDropInfo eventReceiveInfo) => OnEventReceive?.Invoke(eventReceiveInfo);
        [JSInvokable] public void EventLeave(ExternalEventDropInfo eventLeaveInfo) => OnEventLeave?.Invoke(eventLeaveInfo);
        [JSInvokable] public void EventResizeStart(EventDragInfo eventResizeInfo) => OnEventResizeStart?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResizeStop(EventDragInfo eventResizeInfo) => OnEventResizeStop?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResize(EventResizeInfo eventResizeInfo) => OnEventResize?.Invoke(eventResizeInfo);
        [JSInvokable] public void MoreLinkClickMethod(MoreLinkClickInfo info) => OnMoreLinkClick?.Invoke(info);
        // [JSInvokable] public IEnumerable<string> MoreLinkClassNames(int num, string text) => OnMoreLinkClassNames?.Invoke(num, text) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object MoreLinkContent(int num, string text) => OnMoreLinkContent?.Invoke(num, text); // Todo
        [JSInvokable] public void MoreLinkDidMount(int num, string text) => OnMoreLinkDidMount?.Invoke(num, text);
        [JSInvokable] public void MoreLinkWillUnmount(int num, string text) => OnMoreLinkWillUnmount?.Invoke(num, text);

        #endregion

        // Lifecycle methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var calendarData = new
            {
                #region Overall Display

                HeaderToolbar,
                OmitHeaderToolbar,
                FooterToolbar,
                OmitFooterToolbar,
                TitleFormat,
                TitleRangeSeparator,
                ButtonText,
                ButtonIcons,
                CustomButtons,
                ThemeSystem,
                Height,
                ContentHeight,
                AspectRatio,
                ExpandRows,
                HandleWindowResize,
                WindowResizeDelay,
                StickyHeaderDates,
                StickyFooterScrollbar,

                #endregion

                #region Views

                FixedWeekCount,
                ShowNonCurrentDates,
                EventMinHeight,
                EventShortHeight,
                SlotEventOverlap,
                AllDaySlot,
                ListDayFormat,
                OmitListDayFormat,
                ListDaySideFormat,
                OmitListDaySideFormat,
                Duration,
                DayCount,
                VisibleRange,
                InitialView,

                #endregion

                #region Date and Time

                Weekends,
                HiddenDays,
                DayHeaders,
                DayHeaderFormat,
                DayMinWidth,
                SlotDuration,
                SlotLabelInterval,
                SlotLabelFormat,
                SlotMinTime,
                SlotMaxTime,
                ScrollTime,
                ScrollTimeReset,
                InitialDate,
                DateIncrement,
                DateAlignment,
                ValidRange,
                NavLinks,
                NavLinkDayClick,
                NavLinkWeekClick,
                WeekNumbers,
                WeekNumberCalculation,
                WeekText,
                WeekNumberFormat,
                Selectable,
                SelectMirror,
                UnselectAuto,
                UnselectCancel,
                SelectOverlap,
                SelectConstraint,
                SelectMinDistance,
                NowIndicator,
                Now,
                BusinessHours,

                #endregion

                #region Event

                Events,
                DefaultAllDay,
                DefaultAllDayEventDuration,
                DefaultTimedEventDuration,
                ForceEventDuration,
                EventColor,
                EventBackgroundColor,
                EventBorderColor,
                EventTextColor,
                EventDisplay,
                EventTimeFormat,
                DisplayEventTime,
                DisplayEventEnd,
                NextDayThreshold,
                EventOrder,
                EventOrderStrict,
                ProgressiveEventRendering,
                Editable,
                EventStartEditable,
                EventResizableFromStart,
                EventDurationEditable,
                EventResourceEditable,
                Droppable,
                EventDragMinDistance,
                DragRevertDuration,
                DragScroll,
                SnapDuration,
                AllDayMaintainDuration,
                EventOverlap,
                EventConstraint,
                DayMaxEventRows,
                LimitDayEventRowsToHeight,
                DayMaxEvents,
                LimitDayEventsToHeight,
                EventMaxStack,
                MoreLinkClick,
                DayPopoverFormat,

                #endregion

                #region International

                Locale,
                Direction,
                FirstDay,
                TimeZone,

                #endregion

                UseGetVisibleRange = OnGetVisibleRange != null,
                UseNavLinkDayClickMethod = OnNavLinkDayClick != null,
                UseNavLinkWeekClickMethod = OnNavLinkWeekClick != null,
                UseGetWeekNumber = OnGetWeekNumber != null,
                UseGetSelectOverlap = OnGetSelectOverlap != null,
                UseGetSelectAllow = OnGetSelectAllow != null,
                UseGetNow = OnGetNow != null,
                UseGetEventOrder = OnGetEventOrder != null,
                UseEventOverlapMethod = OnEventOverlap != null,
                UseDropAcceptMethod = OnDropAccept != null,
                UseMoreLinkClickMethod = OnMoreLinkClick != null,
            };
            await JsInterop.Render(Id, calendarData, DotNetObjectReference.Create(this));
        }
    }
}
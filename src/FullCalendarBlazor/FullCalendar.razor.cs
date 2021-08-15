using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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
        private readonly Dictionary<PropertyInfo, string> _calendarProperties;
        private readonly Dictionary<PropertyInfo, (string jsName, string dotnetName)> _calendarMethods;

        public FullCalendar()
        {
            _calendarProperties = new Dictionary<PropertyInfo, string>();
            _calendarMethods = new Dictionary<PropertyInfo, (string, string)>();

            foreach (var property in GetType().GetProperties())
            {
                if (property.Name.StartsWith("OnGet"))
                    _calendarMethods.Add(property, ($"{property.Name.Substring(5, 1).ToLower()}{property.Name.Substring(6)}", $"_{property.Name.Substring(2)}"));

                else if (property.Name.StartsWith("On"))
                    _calendarMethods.Add(property, ($"{property.Name.Substring(2, 1).ToLower()}{property.Name.Substring(3)}", $"_{property.Name.Substring(2)}"));

                else
                    _calendarProperties.Add(property, $"{property.Name.Substring(0, 1).ToLower()}{property.Name.Substring(1)}");
            }
        }

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
        [Parameter] public bool? HandleWindowResize { get; set; }
        [Parameter] public int? WindowResizeDelay { get; set; }
        [Parameter] public bool? StickyHeaderDates { get; set; }
        [Parameter] public bool? StickyFooterScrollbar { get; set; }
        [Parameter] public Action<WindowResizeInfo> OnWindowResize { get; set; }

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
        [Parameter] public Action<AllDayInfo> OnAllDayDidMount { get; set; }
        [Parameter] public Action<AllDayInfo> OnAllDayWillUnmount { get; set; }
        [Parameter] public DateTimeFormatter ListDayFormat { get; set; }
        [Parameter] public bool? OmitListDayFormat { get; set; }
        [Parameter] public DateTimeFormatter ListDaySideFormat { get; set; }
        [Parameter] public bool? OmitListDaySideFormat { get; set; }
        // Todo: Add OnNoEventsClassNames EventCallback
        // Todo: Add OnNoEventsContent EventCallback
        [Parameter] public Action<NoEventsInfo> OnNoEventsDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<NoEventsInfo> OnNoEventsWillUnmount { get; set; } // Todo: Replace object with proper type
        [Parameter] public TimeSpan? Duration { get; set; }
        [Parameter] public int? DayCount { get; set; }
        [Parameter] public DateRange VisibleRange { get; set; }
        [Parameter] public Func<DateTime, DateRange> OnGetVisibleRange { get; set; }
        [Parameter] public string InitialView { get; set; }
        [Parameter] public Dictionary<string, object> Views { get; set; }
        // Todo: Add OnViewClassNames EventCallback
        [Parameter] public Action<ViewInfo> OnViewDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<ViewInfo> OnViewWillUnmount { get; set; } // Todo: Replace object with proper type

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
        [Parameter] public Action<DateInfo> OnDatesSet { get; set; }
        [Parameter] public DateTime? InitialDate { get; set; }
        [Parameter] public TimeSpan? DateIncrement { get; set; }
        [Parameter] public DateAlignmentOption? DateAlignment { get; set; }
        [Parameter] public DateRange ValidRange { get; set; }
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
        [Parameter] public Action<WeekNumberInfo> OnWeekNumberDidMount { get; set; }
        [Parameter] public Action<WeekNumberInfo> OnWeekNumberWillUnmount { get; set; }
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
        [Parameter] public Func<object, Event> OnGetEventDataTransform { get; set; }
        [Parameter] public bool? DefaultAllDay { get; set; }
        [Parameter] public TimeSpan? DefaultAllDayEventDuration { get; set; }
        [Parameter] public TimeSpan? DefaultTimedEventDuration { get; set; }
        [Parameter] public bool? ForceEventDuration { get; set; }
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
        [Parameter] public Func<Event, Event, bool> OnGetEventOverlap { get; set; }
        [Parameter] public object EventConstraint { get; set; }
        [Parameter] public Func<EventAllowInfo, Event, bool> OnGetEventAllow { get; set; }
        [Parameter] public string DropAccept { get; set; }
        [Parameter] public Func<object, bool> OnGetDropAccept { get; set; }
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
        [Parameter] public Action<MoreLinkInfo> OnMoreLinkDidMount { get; set; }
        [Parameter] public Action<MoreLinkInfo> OnMoreLinkWillUnmount { get; set; }

        #endregion

        #region International

        [Parameter] public string Locale { get; set; }
        [Parameter] public DirectionOption? Direction { get; set; }
        [Parameter] public DayOfWeek? FirstDay { get; set; }
        [Parameter] public string TimeZone { get; set; }

        #endregion

        // JSInvokable methods

        #region Overall Display

        [JSInvokable] public void _WindowResize(WindowResizeInfo windowResizeInfo) => OnWindowResize?.Invoke(windowResizeInfo);

        #endregion

        #region Views

        [JSInvokable] public void _AllDayDidMount(AllDayInfo allDayInfo) => OnAllDayDidMount?.Invoke(allDayInfo);
        [JSInvokable] public void _AllDayWillUnmount(AllDayInfo allDayInfo) => OnAllDayWillUnmount?.Invoke(allDayInfo);
        [JSInvokable] public void _NoEventsDidMount(NoEventsInfo noEventsInfo) => OnNoEventsDidMount?.Invoke(noEventsInfo);
        [JSInvokable] public void _NoEventsWillUnmount(NoEventsInfo noEventsInfo) => OnNoEventsWillUnmount?.Invoke(noEventsInfo);
        [JSInvokable] public DateRange _GetVisibleRange(DateTime currentDate) => OnGetVisibleRange?.Invoke(currentDate);
        [JSInvokable] public void _ViewDidMount(ViewInfo viewInfo) => OnViewDidMount?.Invoke(viewInfo);
        [JSInvokable] public void _ViewWillUnmount(ViewInfo viewInfo) => OnViewWillUnmount?.Invoke(viewInfo);

        #endregion Views

        #region Date and Time

        [JSInvokable] public void _DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderDidMount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void _DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderWillUnmount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void _DayCellDidMount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellDidMount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void _DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellWillUnmount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void _SlotLabelDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLabelDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLabelWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _SlotLaneDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLaneDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLaneWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void _DatesSet(DateInfo dateInfo) => OnDatesSet?.Invoke(dateInfo);
        [JSInvokable] public void _NavLinkDayClick(DateTime date, object jsEvent) => OnNavLinkDayClick?.Invoke(date, jsEvent);
        [JSInvokable] public void _NavLinkWeekClick(DateTime weekStart, object jsEvent) => OnNavLinkWeekClick?.Invoke(weekStart, jsEvent);
        [JSInvokable] public int? _GetWeekNumber(DateTime date) => OnGetWeekNumber?.Invoke(date);
        [JSInvokable] public void _WeekNumberDidMount(WeekNumberInfo weekNumberInfo) => OnWeekNumberDidMount?.Invoke(weekNumberInfo);
        [JSInvokable] public void _WeekNumberWillUnmount(WeekNumberInfo weekNumberInfo) => OnWeekNumberWillUnmount?.Invoke(weekNumberInfo);
        [JSInvokable] public bool? _GetSelectOverlap(Event overlappedEvent) => OnGetSelectOverlap?.Invoke(overlappedEvent);
        [JSInvokable] public bool? _GetSelectAllow(SelectInfo selectInfo) => OnGetSelectAllow?.Invoke(selectInfo);
        [JSInvokable] public void _DateClick(DateClickInfo dateClickInfo) => OnDateClick?.Invoke(dateClickInfo);
        [JSInvokable] public void _Select(SelectionInfo selectionInfo) => OnSelect?.Invoke(selectionInfo);
        [JSInvokable] public void _Unselect(object jsEvent, View view) => OnUnselect?.Invoke(jsEvent, view);
        [JSInvokable] public DateTime? _GetNow() => OnGetNow?.Invoke();
        [JSInvokable] public void _NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorDidMount?.Invoke(nowIndicatorInfo);
        [JSInvokable] public void _NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorWillUnmount?.Invoke(nowIndicatorInfo);

        #endregion

        #region Event

        [JSInvokable] public object _GetEventDataTransform(object eventData) => (object) OnGetEventDataTransform?.Invoke(eventData) ?? false;
        [JSInvokable] public void _EventAdd(EventAddInfo eventAddInfo) => OnEventAdd?.Invoke(eventAddInfo);
        [JSInvokable] public void _EventRemove(EventChangeInfo eventChangeInfo) => OnEventChange?.Invoke(eventChangeInfo);
        [JSInvokable] public void _EventChange(EventAddInfo eventRemoveInfo) => OnEventRemove?.Invoke(eventRemoveInfo);
        [JSInvokable] public void _EventsSet(IEnumerable<Event> events) => OnEventsSet?.Invoke(events);
        [JSInvokable] public int? _GetEventOrder(Event eventA, Event eventB) => OnGetEventOrder?.Invoke(eventA, eventB);
        [JSInvokable] public void _EventDidMount(EventRenderInfo eventRenderInfo) => OnEventDidMount?.Invoke(eventRenderInfo);
        [JSInvokable] public void _EventWillUnmount(EventRenderInfo eventRenderInfo) => OnEventWillUnmount?.Invoke(eventRenderInfo);
        [JSInvokable] public void _EventClick(EventClickInfo eventClickInfo) => OnEventClick?.Invoke(eventClickInfo);
        [JSInvokable] public void _EventMouseEnter(EventClickInfo mouseEnterInfo) => OnEventMouseEnter?.Invoke(mouseEnterInfo);
        [JSInvokable] public void _EventMouseLeave(EventClickInfo mouseLeaveInfo) => OnEventMouseLeave?.Invoke(mouseLeaveInfo);
        [JSInvokable] public bool _GetEventOverlap(Event stillEvent, Event movingEvent) => OnGetEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool _GetEventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent) => OnGetEventAllow?.Invoke(eventAllowInfo, draggedEvent) ?? true;
        [JSInvokable] public bool _GetDropAccept(object draggableItem) => OnGetDropAccept?.Invoke(draggableItem) ?? true; // Todo: Replace object with DraggableItem type.
        [JSInvokable] public void _EventDragStart(EventDragInfo eventDragInfo) => OnEventDragStart?.Invoke(eventDragInfo);
        [JSInvokable] public void _EventDragStop(EventDragInfo eventDragInfo) => OnEventDragStop?.Invoke(eventDragInfo);
        [JSInvokable] public void _EventDrop(EventDropInfo eventDropInfo) => OnEventDrop?.Invoke(eventDropInfo);
        [JSInvokable] public void _Drop(DropInfo dropInfo) => OnDrop?.Invoke(dropInfo);
        [JSInvokable] public void _EventReceive(ExternalEventDropInfo eventReceiveInfo) => OnEventReceive?.Invoke(eventReceiveInfo);
        [JSInvokable] public void _EventLeave(ExternalEventDropInfo eventLeaveInfo) => OnEventLeave?.Invoke(eventLeaveInfo);
        [JSInvokable] public void _EventResizeStart(EventDragInfo eventResizeInfo) => OnEventResizeStart?.Invoke(eventResizeInfo);
        [JSInvokable] public void _EventResizeStop(EventDragInfo eventResizeInfo) => OnEventResizeStop?.Invoke(eventResizeInfo);
        [JSInvokable] public void _EventResize(EventResizeInfo eventResizeInfo) => OnEventResize?.Invoke(eventResizeInfo);
        [JSInvokable] public void _MoreLinkClick(MoreLinkClickInfo info) => OnMoreLinkClick?.Invoke(info);
        [JSInvokable] public void _MoreLinkDidMount(MoreLinkInfo moreLinkInfo) => OnMoreLinkDidMount?.Invoke(moreLinkInfo);
        [JSInvokable] public void _MoreLinkWillUnmount(MoreLinkInfo moreLinkInfo) => OnMoreLinkWillUnmount?.Invoke(moreLinkInfo);

        #endregion

        // Public methods

        #region Overall Display

        public async Task UpdateSizeAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "updateSize");

        #endregion

        #region Views

        public async Task<View> GetViewAsync() => await JsInterop.GetPropertyAsync<View>(Id, "view");
        public async Task ChangeViewAsync(string viewOption) =>  await JsInterop.ExecuteVoidMethodAsync(Id, "changeView", viewOption);
        public async Task ChangeViewAsync(string viewOption, DateTime date) =>  await JsInterop.ExecuteVoidMethodAsync(Id, "changeView", viewOption, date);

        #endregion

        #region Date and Time

        public async Task SetScrollToTimeAsync(TimeSpan duration) => await JsInterop.ExecuteVoidMethodAsync(Id, "scrollToTime", duration);
        public async Task GoToPrevAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "prev");
        public async Task GoToNextAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "next");
        public async Task GoToPrevYearAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "prevYear");
        public async Task GoToNextYearAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "nextYear");
        public async Task GoToTodayAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "today");
        public async Task GoToDateAsync(DateTime date) => await JsInterop.ExecuteVoidMethodAsync(Id, "gotoDate", date);
        public async Task IncrementDateAsync(TimeSpan duration) => await JsInterop.ExecuteVoidMethodAsync(Id, "incrementDate", duration);
        public async Task<DateTime> GetDateAsync() => await JsInterop.ExecuteMethodAsync<DateTime>(Id, "getDate");
        public async Task SelectPeriodAsync(DateTime start) => await JsInterop.ExecuteVoidMethodAsync(Id, "select", start);
        public async Task SelectPeriodAsync(DateTime start, DateTime end) => await JsInterop.ExecuteVoidMethodAsync(Id, "select", start, end);
        public async Task SelectPeriodAsync(SelectInfo selectInfo) => await JsInterop.ExecuteVoidMethodAsync(Id, "select", selectInfo); // Todo: All day prop

        public async Task UnselectPeriodAsync() => await JsInterop.ExecuteVoidMethodAsync(Id, "unselect");

        #endregion

        #region Event

        public async Task<IEnumerable<Event>> GetEvents() => await JsInterop.ExecuteMethodAsync<IEnumerable<Event>>(Id, "getEvents");
        public async Task<Event> GetEventById(string id) => await JsInterop.ExecuteMethodAsync<Event>(Id, "getEventById", id);
        public async Task<Event> AddEvent(Event newEvent) => await JsInterop.ExecuteMethodAsync<Event>(Id, "addEvent", newEvent); // Todo: sources

        #endregion

        // Lifecycle methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var calendarData = new ExpandoObject() as IDictionary<string, object>;
            var calendarMethods = new List<(string, string)>();

            foreach (var (property, jsName) in _calendarProperties)
            {
                var value = property.GetValue(this);
                if (value != null)
                    calendarData.Add(jsName, value);
            }

            foreach (var (property, (jsName, dotnetName)) in _calendarMethods)
            {
                var value = property.GetValue(this);
                if (value != null)
                    calendarMethods.Add((jsName, dotnetName));
            }

            await JsInterop.RenderAsync(calendarData, calendarMethods, DotNetObjectReference.Create(this));
        }
    }
}
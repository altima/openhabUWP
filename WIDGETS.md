# Widget

| Property | Type   | Sample value  |
|----------|--------|---------------|
| widgetId | string |               |
| type     | string |               |
| label    | string |               |
| icon     | string |               |
| mappings | array of [Mapping]  |     |
| item     | object |               |
| minValue     | number |               |
| maxValue     | number |               |
| step     | number |               |
| widgets     | array of [Widget] |               |
|switchSupport | boolean | true|
|sendFrequency | number | 0|

## Switch::[Widget]
## Chart::[Widget]
## Frame::[Widget]
## Selection::[Widget]
## Setpoint::[Widget]
## Slider::[Widget]
## Colorpicker::[Widget]
## Mapview::[Widget]
## Image::[Widget]
## Video::[Widget]
## Webview::[Widget]

# Item

| Property | Type   | Sample value  |
|----------|--------|---------------|
| link     | string |               |
| state    | string |               |
| stateDescription | [object](#statedescription) |               |
| type     | string |               |
| name     | string |               |
| label    | string |               |
| category | string |               |
| tags     | array  | []            |
| groups   | array  | []            |

### StateDescription

| Property | Type   | Sample value  |
|----------|--------|---------------|
| pattern  | string | %.1f Â°C      |
| readonly    | boolean | false     |
| options | array |                 |


## SwitchItem::[Item]

## NumberItem::[Item]


# Mapping

| Property | Type   | Sample value  |
|----------|--------|---------------|
| command  | string | ON            |
| label    | string | On            |




[Widget]: #widget
[Switch]: #switch::widget
[Chart]: #chart::widget
[Frame]: #frame::widget
[Selection]: #selection::widget
[Setpoint]: #setpoint::widget
[Slider]: #slider::widget
[Colorpicker]: #colorpicker::widget
[Mapview]: #mapview::widget
[Image]: #image::widget
[Video]: #video::widget
[Webview]: #webview::widget

[Item]: #item

[Mapping]: #mapping

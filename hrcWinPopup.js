Xoffset = -60;
Yoffset = 20;
var old, skn, iex = (document.all), yyy = -1000;
var ns4 = document.layers
var ns6 = document.getElementById && !document.all
var ie4 = document.all
function popup(msg, bak) {
document.onmousemove = get_mouse;
if (ns4)
   skn = document.dek
else if (ns6)
   skn = document.getElementById("dek").style
else if (ie4)
  skn = document.all.dek.style
if (ns4) document.captureEvents(Event.MOUSEMOVE);
 else {
skn.visibility = "visible"
skn.display = "none"
 }
 var content = "<table width=170 border=1 bordercolor=#333333 cellpadding=2 cellspacing=0 " +
  "bgcolor=" + bak + "><td align=center><font color=black size=1>" + msg + "</font></td></table>";
 yyy = Yoffset;
if (ns4) { skn.document.write(content); skn.document.close(); skn.visibility = "visible" }
if (ns6) { document.getElementById("dek").innerHTML = content; skn.display = '' }
if (ie4) { document.all("dek").innerHTML = content; skn.display = '' }
}
function get_mouse(e) {
var x = (ns4 || ns6) ? e.pageX : event.x + document.body.scrollLeft;
skn.left = x + Xoffset + "px";
var y = (ns4 || ns6) ? e.pageY : event.y + document.body.scrollTop;
skn.top = y + Yoffset + "px";
}
function kill() {
yyy = -1000;
skn = document.getElementById("dek").style
if (ns4) { skn.visibility = "hidden"; }
else if (ns6 || ie4)
skn.display = "none"
}


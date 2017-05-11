class corsMiddleware(object):
    def process_response(self, req, resp):
        resp["Access-Control-Allow-Headers"] = "Authorization, Content-Type, X-CSRFToken"
        resp["Access-Control-Allow-Credentials"] = "true"
        resp["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS"
        resp['Access-Control-Allow-Origin'] = req.META['HTTP_ORIGIN'] if 'HTTP_ORIGIN' in req.META else None

        return resp

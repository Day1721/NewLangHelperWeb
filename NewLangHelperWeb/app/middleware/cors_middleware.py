class corsMiddleware(object):
    def process_response(self, req, resp):
        resp["Access-Control-Allow-Headers"] = "*"
        return resp